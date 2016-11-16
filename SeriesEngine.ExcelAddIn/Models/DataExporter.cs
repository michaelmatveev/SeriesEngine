using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models.Fragments;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Data;
using System.IO;
using SeriesEngine.ExcelAddIn.Helpers;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataExporter : BaseDataExporter,
        ICommand<SaveAllCommandArgs>
    {
        private Workbook _workbook;
        private readonly IFragmentsProvider _fragmentsProvider;
        private readonly INetworksProvider _networksProvider;
        private Random _random = new Random();

        public DataExporter(Workbook workbook, IFragmentsProvider fragmentsProvider, INetworksProvider networksProvider)
        {
            _workbook = workbook;
            _fragmentsProvider = fragmentsProvider;
            _networksProvider = networksProvider;
        }

        public void Execute(SaveAllCommandArgs commandData)
        {
            var fragmentsToExport = _fragmentsProvider.GetFragments(string.Empty).OfType<SheetFragment>();
            ExportFromFragments(fragmentsToExport);
        }

        private class ElementInfo
        {
            public string RefObject { get; private set; }
            public string FieldName { get; private set; }
            public string ParentName { get; private set; }
            public int Index { get; private set; }

            public ElementInfo(SubFragment sf, int index)
            {
                RefObject = sf.RefObject;
                Index = index;
                var nsf = sf as NodeSubFragment;
                string fieldName = string.Empty;
                if (nsf != null)
                {
                    var endIndex = sf.XmlPath.LastIndexOf('@');
                    FieldName = sf.XmlPath.Substring(endIndex + 1, sf.XmlPath.Length - (endIndex + 1));
                }

                var vsf = sf as VariableSubFragment;
                if (vsf != null)
                {
                    var endIndex = sf.XmlPath.LastIndexOf('/');
                    FieldName = sf.XmlPath.Substring(endIndex + 1, sf.XmlPath.Length - (endIndex + 1));
                    ParentName = sf.XmlPath.Split('/').Reverse().Skip(2).First();
                }

                ParentName = sf.XmlPath.Split('/').Reverse().Skip(2).First();

            }

            public ElementInfo(string refObjectName, string parentName)
            {
                RefObject = refObjectName;
                ParentName = parentName;
            }

            public override bool Equals(object obj)
            {
                var other = (ElementInfo)obj;
                return this.RefObject == other.RefObject && this.ParentName == other.ParentName;
            }

            public override int GetHashCode()
            {
                return this.RefObject.GetHashCode() + this.ParentName.GetHashCode();
            }

        } 

        public override void ExportFragment(ObjectGridFragment fragment)
        {
            var network = _networksProvider.GetNetworks(string.Empty).First() as NetworkTree;

            var doc = new XDocument(new XElement("DataToImport"));
            Excel.Worksheet sheet = _workbook.Sheets[fragment.Sheet];
            var listObject = sheet.ListObjects.Cast<Excel.ListObject>().SingleOrDefault(l => l.Name == fragment.Name);          

            var schema = fragment.GetSchema();
            using (var sr = new StringReader(schema))
            {
                var dataSet = new DataSet();
                dataSet.ReadXmlSchema(sr);

                var tree = fragment
                    .SubFragments
                    .Select((f, i) => new ElementInfo(f, i))
                    .GroupBy(ei => new ElementInfo(ei.RefObject, ei.ParentName))
                    .GenerateTree(n => n.Key.RefObject, p => p.Key.ParentName, "DataToImport");

                for (int row = 1; row <= listObject.DataBodyRange.Rows.Count; row++)
                {
                    InsertRowInDataSet(row, dataSet, listObject, 0, tree);
                }
                    //for (int row = 1; row <= listObject.DataBodyRange.Rows.Count; row++)
                    //{
                    //    foreach (var groupOfExtendedInfos in fragment
                    //        .SubFragments
                    //        .OrderBy(sf => sf.Level)                        
                    //        .Select((f, i) => new ElementInfo(f, i))
                    //        .GroupBy(f => new { f.RefObject, f.ParentName }))
                    //    {
                    //        var dataRow = dataSet.Tables[groupOfExtendedInfos.Key.RefObject].NewRow();
                    //        foreach (var ei in groupOfExtendedInfos)
                    //        {
                    //            var cellValue = (listObject.DataBodyRange[row, ei.Index + 1] as Excel.Range).Value2;
                    //            dataRow[ei.FieldName] = cellValue;
                    //        }

                    //        if (groupOfExtendedInfos.Key.ParentName != "DataToImport")
                    //        {
                    //            //var parentIndex = fragment.SubFragments.IndexOf
                    //            var parentValue = (listObject.DataBodyRange[row, parentIndex + 1] as Excel.Range).Value2;
                    //            var filter = $"{groupOfExtendedInfos.Key.ParentName}_UniqueName = '{parentValue}'";
                    //            int parentId = (int)dataSet.Tables[groupOfExtendedInfos.Key.ParentName].Select(filter).Single()["Id"];
                    //            dataRow[$"{groupOfExtendedInfos.Key.ParentName}_Id"] = parentId;
                    //        }

                    //        dataSet.Tables[groupOfExtendedInfos.Key.RefObject].Rows.Add(dataRow);
                    //    }
                    //}

                    var s = dataSet.GetXml();
            }
            

            //for (int row = 1; row <= listObject.DataBodyRange.Rows.Count; row++)
            //{
            //    //for (int col = 1; col <= listObject.DataBodyRange.Columns.Count; col++)
            //    foreach (var sf in fragment.SubFragments)
            //    {
            //        ConstructForSubFragment(doc, listObject, row, sf, fragment.SubFragments);
            //    }
            //}

            network.LoadFromXml(doc);
        }

        private void InsertRowInDataSet(int row, DataSet dataSet, Excel.ListObject listObject, int rootId, IEnumerable<TreeItem<IGrouping<ElementInfo, ElementInfo>>> currentItems)
        {
            foreach(var node in currentItems)
            {
                var dataRow = dataSet.Tables[node.Item.Key.RefObject].NewRow();
                foreach (var ei in node.Item)
                {
                    var cellValue = (listObject.DataBodyRange[row, ei.Index + 1] as Excel.Range).Value2;
                    dataRow[ei.FieldName] = cellValue;
                }

                if (node.Item.Key.ParentName != "DataToImport")
                {
                    dataRow[$"{node.Item.Key.ParentName}_Id"] = rootId;
                }

                dataSet.Tables[node.Item.Key.RefObject].Rows.Add(dataRow);
                //dataSet.AcceptChanges();

                if (node.Children.Any())
                {
                    int id = (int)dataRow[$"{node.Item.Key.RefObject}_Id"];
                    InsertRowInDataSet(row, dataSet, listObject, id, node.Children);
                }

            }

        }

        //private void InsertRowInDataSet(int row, DataSet dataSet, Excel.ListObject listObject, IEnumerable<TreeItem<ElementInfo>> currentItems)
        //{
        //    foreach (var groupOfExtendedInfos in currentItems.GroupBy(f => f.Item.RefObject))
        //    {
        //        var dataRow = dataSet.Tables[groupOfExtendedInfos.Key].NewRow();
        //        foreach (var ei in groupOfExtendedInfos)
        //        {
        //            var cellValue = (listObject.DataBodyRange[row, ei.Item.Index + 1] as Excel.Range).Value2;
        //            dataRow[ei.Item.FieldName] = cellValue;
        //        }

        //        var current = currentItems.Single(s => s.Item.RefObject == groupOfExtendedInfos.Key);

        //        if (current.Item.ParentName != "DataToImport")
        //        {
        //            var parentValue = (listObject.DataBodyRange[row, current.Item. + 1] as Excel.Range).Value2;
        //            var filter = $"{groupOfExtendedInfos.Key.ParentName}_UniqueName = '{parentValue}'";
        //            int parentId = (int)dataSet.Tables[groupOfExtendedInfos.Key.ParentName].Select(filter).Single()["Id"];
        //            dataRow[$"{groupOfExtendedInfos.Key.ParentName}_Id"] = parentId;
        //        }

        //        dataSet.Tables[groupOfExtendedInfos.Key].Rows.Add(dataRow);

        //        InsertRowInDataSet(row, dataSet, listObject, current.Children);
        //    }
        //}

        private string GetParentPath(SubFragment sf)
        {
            //return "parent::" + sf.XmlPath.Split('@').First();
            var end = sf.XmlPath.LastIndexOf('/');
            return sf.XmlPath.Substring(0, end);
        }

        private XElement ConstructForSubFragment(XDocument doc, Excel.ListObject listObject, int row, SubFragment sf, IList<SubFragment> subFragments)
        {
            int col = subFragments.IndexOf(sf) + 1;

            var cellValue = (listObject.DataBodyRange[row, col] as Excel.Range).Value2;
            XElement element = null;
            string parentPath = null;

            var nsf = sf as NodeSubFragment;
            if (nsf != null)
            {
                if (nsf.NodeType == NodeType.UniqueName)
                {
                    var endIndex = sf.XmlPath.LastIndexOf('/'); //remove attribure name
                    var elementPath = sf.XmlPath.Substring(0, endIndex);
                    var attribute = sf.XmlPath.Substring(endIndex + 1, sf.XmlPath.Length - (endIndex + 1));

                    //                foreach(var nsf1 in subFragments.OfType<NodeSubFragment>().Where(n => n.XmlPath.StartsWith(elementPath + "/@"))) { 
                    //}

                    element = doc.XPathSelectElement($"{elementPath}[{attribute}='{cellValue}']");
                    //element = doc.XPathSelectElement(elementPath);
                    if (element == null)
                    {
                        element = new XElement(sf.RefObject);
                        element.Add(new XAttribute("UniqueName", cellValue));
                    }
                    else
                    {
                        return null;
                    }

                    //switch (nsf.NodeType)
                    //{
                    //    case NodeType.UniqueName:
                    //        element.Add(new XAttribute("UniqueName", cellValue));
                    //        break;
                        //case NodeType.Since:
                        //    element.Add(new XAttribute("Since", cellValue));
                        //    break;
                        //case NodeType.Till:
                        //    element.Add(new XAttribute("Till", cellValue));
                        //    break;
                    //    default:
                    //        throw new NotSupportedException("this operation is not supported");
                    //}

                    endIndex = elementPath.LastIndexOf('/'); // remove current element name
                    parentPath = sf.XmlPath.Substring(0, endIndex);
                }
                else
                {
                    return null;
                }
            }

            // одна переменная не может быть связана с двумя столбцами???
            var vsf = sf as VariableSubFragment;
            if (vsf != null)
            {
                element = doc.XPathSelectElement(sf.XmlPath);
                if (element == null)
                {
                    element = new XElement(vsf.VariableName, cellValue);
                }

                var endIndex = sf.XmlPath.LastIndexOf('/');
                var elementPath = sf.XmlPath.Substring(0, endIndex);

                parentPath = $"{elementPath}[{row}]";

                //var endIndex = sf.XmlPath.LastIndexOf('/');
                //parentPath = sf.XmlPath.Substring(0, endIndex);
            }

            if(parentPath == "/DataToImport")
            {
                doc.Root.Add(element);
                return doc.Root;
            }

            var parent = doc.XPathSelectElement(parentPath);
            if (parent == null)
            {
                var parentSf = subFragments.First(p => p.XmlPath.StartsWith(parentPath));
                parent = ConstructForSubFragment(doc, listObject, row, parentSf, subFragments);
            }

            parent.Add(element);
            return parent;
        }
    }
}
