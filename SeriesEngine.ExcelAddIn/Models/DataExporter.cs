using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using SeriesEngine.ExcelAddIn.Models.Fragments;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using System.Xml.Linq;
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

        public override void ExportFragment(ObjectGridFragment fragment)
        {
            var network = _networksProvider.GetNetworks(string.Empty).First() as NetworkTree;
            Excel.Worksheet sheet = _workbook.Sheets[fragment.Sheet];
            var listObject = sheet.ListObjects.Cast<Excel.ListObject>().SingleOrDefault(l => l.Name == fragment.Name);
            // http://stackoverflow.com/questions/12572439/why-does-readxmlschema-create-extra-id-column
            var schema = fragment.GetSchema();
            using (var sr = new StringReader(schema))
            {
                var dataSet = new DataSet();
                dataSet.ReadXmlSchema(sr);

                var tree = fragment
                    .SubFragments
                    .Select((f, i) => new ElementInfo(f, i))
                    .GroupBy(ei => new ElementInfo(ei.RefObject, ei.ParentName))
                    .GenerateTree(n => n.Key.RefObject, p => p.Key.ParentName, NetworkTree.RootName);

                for (int row = 1; row <= listObject.DataBodyRange.Rows.Count; row++)
                {
                    InsertRowInDataSet(row, dataSet, listObject, 0, tree);
                }

                var doc = XDocument.Parse(dataSet.GetXml());
                network.LoadFromXml(doc);
            }
        }

        private void InsertRowInDataSet(int row, DataSet dataSet, Excel.ListObject listObject, int rootId, IEnumerable<TreeItem<IGrouping<ElementInfo, ElementInfo>>> currentItems)
        {
            foreach (var node in currentItems)
            {
                var table = dataSet.Tables[node.Item.Key.RefObject];
                var dataRow = table.NewRow();
                foreach (var ei in node.Item)
                {
                    var cellValue = (listObject.DataBodyRange[row, ei.Index + 1] as Excel.Range).Value2;
                    dataRow[ei.FieldName] = cellValue;
                }

                if (node.Item.Key.ParentName != NetworkTree.RootName)
                {
                    dataRow[$"{node.Item.Key.ParentName}_Id"] = rootId;
                }

                int id = GetRowId(table, dataRow);    
                if (id == -1)
                {
                    dataSet.Tables[node.Item.Key.RefObject].Rows.Add(dataRow);
                }

                if (node.Children.Any())
                {
                    if (id == -1)
                    {
                        id = (int)dataRow[$"{node.Item.Key.RefObject}_Id"];
                    }
                    InsertRowInDataSet(row, dataSet, listObject, id, node.Children);
                }

            }
        }

        private int GetRowId(DataTable table, DataRow row)
        {
            var match = table.AsEnumerable().FirstOrDefault(r => r["UniqueName"].Equals(row["UniqueName"]));
            if(match != null)
            {
                return (int)match[$"{table.TableName}_Id"];
            }
            return -1;
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
    }
}
