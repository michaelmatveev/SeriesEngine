using Microsoft.Office.Tools.Excel;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.ExcelAddIn.Models.Fragments;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataExporter : BaseDataExporter,
        ICommand<SaveAllCommandArgs>
    {
        private Workbook _workbook;
        private readonly IFragmentsProvider _fragmentsProvider;
        private readonly INetworksProvider _networksProvider;

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
            // another way http://stackoverflow.com/questions/12572439/why-does-readxmlschema-create-extra-id-column
            var schema = fragment.GetSchema();
            using (var sr = new StringReader(schema))
            {
                var dataSet = new DataSet();
                dataSet.ReadXmlSchema(sr);

                var tree = fragment
                    .SubFragments
                    .Select((f, i) => new ColumnIdentity(f, i))
                    .GroupBy(ei => new ObjectIdentity(ei.RefObject, ei.Parent))
                    .GenerateTree(n => n.Key.RefObject, p => p.Key.Parent, NetworkTree.RootName);

                for (int row = 1; row <= listObject.DataBodyRange.Rows.Count; row++)
                {
                    InsertRowInDataSet(row, dataSet, listObject, 0, tree);
                }

                var doc = XDocument.Parse(dataSet.GetXml());
                network.LoadFromXml(doc);
            }
        }

        private void InsertRowInDataSet(int row, DataSet dataSet, Excel.ListObject listObject, int rootId, IEnumerable<TreeItem<IGrouping<ObjectIdentity, ColumnIdentity>>> currentItems)
        {
            foreach (var node in currentItems)
            {
                var table = dataSet.Tables[node.Item.Key.RefObject];
                var dataRow = table.NewRow();
                foreach (var column in node.Item)
                {
                    var cellValue = (listObject.DataBodyRange[row, column.Index + 1] as Excel.Range).Value2;
                    dataRow[column.FieldName] = cellValue;
                }

                var rows = table.AsEnumerable();

                if (node.Item.Key.Parent != NetworkTree.RootName)
                {
                    rows = rows.Where(r => r[$"{node.Item.Key.Parent}_Id"].Equals(rootId));
                    dataRow[$"{node.Item.Key.Parent}_Id"] = rootId;
                }
                    
                var match = rows.FirstOrDefault(r => r["UniqueName"].Equals(dataRow["UniqueName"]));
                int id = match != null ? (int)match[$"{table.TableName}_Id"] : -1;

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

        #region Helper classes

        private class ObjectIdentity
        {
            public string RefObject { get; protected set; }
            public string Parent { get; protected set; }

            public ObjectIdentity(string refObjectName, string parentName)
            {
                RefObject = refObjectName;
                Parent = parentName;
            }

            protected ObjectIdentity(SubFragment sf)
            {
                RefObject = sf.RefObject;
                Parent = sf.XmlPath.Split('/').Reverse().Skip(2).First();
            }

            public override bool Equals(object obj)
            {
                var other = (ObjectIdentity)obj;
                return RefObject == other.RefObject && Parent == other.Parent;
            }

            public override int GetHashCode()
            {
                return RefObject.GetHashCode() + Parent.GetHashCode();
            }

        }

        private class ColumnIdentity : ObjectIdentity
        {
            public string FieldName { get; private set; }
            public int Index { get; private set; }

            public ColumnIdentity(SubFragment sf, int index) : base(sf)
            {
                Index = index;
                int start = -1;

                var nsf = sf as NodeSubFragment;
                if (nsf != null)
                {
                    start = sf.XmlPath.LastIndexOf('@');
                }

                var vsf = sf as VariableSubFragment;
                if (vsf != null)
                {
                    start = sf.XmlPath.LastIndexOf('/');
                }

                FieldName = sf.XmlPath.Substring(start + 1, sf.XmlPath.Length - (start + 1));
            }

        }

        #endregion
    }
}
