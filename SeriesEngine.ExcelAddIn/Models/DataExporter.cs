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

            var doc = new XDocument(new XElement("DataToImport"));
            Excel.Worksheet sheet = _workbook.Sheets[fragment.Sheet];
            var listObject = sheet.ListObjects.Cast<Excel.ListObject>().SingleOrDefault(l => l.Name == fragment.Name);          

            var schema = fragment.GetSchema();

            //var xmlMap = _workbook.XmlMaps.Cast<Excel.XmlMap>().SingleOrDefault(m => m.Name == fragment.Name);
            //string xmlData;
            //xmlMap.ExportXml(out xmlData);

            //foreach (var c in listObject.ListColumns.Cast<Excel.ListColumn>())
            //{
            //    var v = c.XPath.Value;
            //}

            for (int row = 1; row <= listObject.DataBodyRange.Rows.Count; row++)
            {
                //for (int col = 1; col <= listObject.DataBodyRange.Columns.Count; col++)
                foreach (var sf in fragment.SubFragments)
                {
                    ConstructForSubFragment(doc, listObject, row, sf, fragment.SubFragments);
                }
            }

            network.LoadFromXml(doc);
        }

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
                var endIndex = sf.XmlPath.LastIndexOf('/'); //remove attribure name
                var elementPath = sf.XmlPath.Substring(0, endIndex);
                var attribute = sf.XmlPath.Substring(endIndex + 1, sf.XmlPath.Length - (endIndex + 1));

                //element = doc.XPathSelectElement($"{elementPath}[{attribute}='{cellValue}']");
                element = doc.XPathSelectElement(elementPath);
                if (element == null)
                {
                    element = new XElement(sf.RefObject);
                }

                switch (nsf.NodeType)
                {
                    case NodeType.UniqueName:
                        element.Add(new XAttribute("UniqueName", cellValue));
                        break;
                    case NodeType.Since:
                        element.Add(new XAttribute("Since", cellValue));
                        break;
                    case NodeType.Till:
                        element.Add(new XAttribute("Till", cellValue));
                        break;
                    default:
                        throw new NotSupportedException("this operation is not supported");
                }
                                
                endIndex = elementPath.LastIndexOf('/'); // remove current element name
                parentPath = sf.XmlPath.Substring(0, endIndex);
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
                parentPath = sf.XmlPath.Substring(0, endIndex);

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
