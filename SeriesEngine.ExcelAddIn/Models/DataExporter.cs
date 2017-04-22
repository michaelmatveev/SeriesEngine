using Microsoft.Office.Tools.Excel;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataExporter : BaseDataExporter,
        ICommand<SaveAllCommandArgs>,
        ICommand<PreserveDataBlocksCommandArgs>
    {
        private readonly Workbook _workbook;
        private readonly IDataBlockProvider _blockProvider;
        private readonly INetworksProvider _networksProvider;

        public DataExporter(Workbook workbook, IDataBlockProvider blockProvider, INetworksProvider networksProvider)
        {
            _workbook = workbook;
            _blockProvider = blockProvider;
            _networksProvider = networksProvider;
        }

        public void Execute(SaveAllCommandArgs commandData)
        {
            using (new ActiveRangeKeeper(_workbook))
            {
                var sheetDataBlocks = _blockProvider.GetDataBlocks().OfType<SheetDataBlock>();
                ExportFromDataBlocks(commandData.Solution, sheetDataBlocks);
                _blockProvider.Save(); // save NetworkRevision
            }
        }

        public override void ExportDataBlock(Solution solution, CollectionDataBlock collectionDatablock)
        {
            //var period = _blockProvider.GetDefaultPeriod(collectionDatablock);
            var networkTree = _networksProvider
                .GetNetwork(solution.Id, collectionDatablock.NetworkName, collectionDatablock.DataBlocks, null);

            Excel.Worksheet sheet = _workbook.Sheets[collectionDatablock.Sheet];
            var listObject = sheet.ListObjects.Cast<Excel.ListObject>().SingleOrDefault(l => l.Name == collectionDatablock.Name);

            var schema = collectionDatablock.GetSchema();
            var sr = new StringReader(schema);

            var dsChanged = new DataSet();
            dsChanged.ReadXmlSchema(sr);

            if (listObject.DataBodyRange != null)
            {
                var tree = collectionDatablock
                    .DataBlocks
                    .Select((f, i) => new ColumnIdentity(f, i))
                    .GroupBy(ci => new ObjectIdentity(ci.RefObject, ci.Parent))
                    .GenerateTree(n => n.Key.RefObject, p => p.Key.Parent, NetworkTree.RootName);

                for (int row = 1; row <= listObject.DataBodyRange.Rows.Count; row++)
                {
                    CreateOrUpdateRowInDataSet(row, dsChanged, listObject, 0, tree);
                }
            }

            //var source = new XDocument(collectionDatablock.Xml ?? networkTree.ConvertToXml(collectionDatablock.DataBlocks, period));
            var source = new XDocument(collectionDatablock.Xml);
            var target = XDocument.Parse(dsChanged.GetXml());
            networkTree.LoadFromXml(source, target);
        }

  
        //public override void ExportDataBlock(int solutionId, CollectionDataBlock collection)
        //{
        //    var network = _networksProvider
        //        .GetNetworks(solutionId)
        //        .SingleOrDefault(n => n.Name == collection.NetworkName);

        //    using (network.GetExportLock(collection))
        //    {
        //        Excel.Worksheet sheet = _workbook.Sheets[collection.Sheet];
        //        var listObject = sheet.ListObjects.Cast<Excel.ListObject>().SingleOrDefault(l => l.Name == collection.Name);
        //        // another way http://stackoverflow.com/questions/12572439/why-does-readxmlschema-create-extra-id-column
        //        var schema = collection.GetSchema();
        //        var sr = new StringReader(schema);

        //        var dsChanged = new DataSet();
        //        dsChanged.ReadXmlSchema(sr);

        //        var tree = collection
        //            .DataBlocks
        //            .Select((f, i) => new ColumnIdentity(f, i))
        //            .GroupBy(ci => new ObjectIdentity(ci.RefObject, ci.Parent))
        //            .GenerateTree(n => n.Key.RefObject, p => p.Key.Parent, NetworkTree.RootName);

        //        for (int row = 1; row <= listObject.DataBodyRange.Rows.Count; row++)
        //        {
        //            CreateOrUpdateRowInDataSet(row, dsChanged, listObject, 0, tree);
        //        }

        //        var doc = XDocument.Parse(dsChanged.GetXml());
        //        network.LoadFromXml(doc);
        //        //network.UpdateVariables();
        //    }
        //}

        private void CreateOrUpdateRowInDataSet(int row, DataSet dataSet, Excel.ListObject listObject, int rootId, IEnumerable<TreeItem<IGrouping<ObjectIdentity, ColumnIdentity>>> currentItems)
        {
            foreach (var node in currentItems)
            {
                var table = dataSet.Tables[node.Item.Key.RefObject];
                var nameColumn = node.Item.SingleOrDefault(c => c.FieldName == "UniqueName");
                var objectName = (listObject.DataBodyRange[row, nameColumn.Index + 1] as Excel.Range).Value2;
                if (objectName == null)
                {
                    return;
                }

                var rows = table.AsEnumerable();
                if (node.Item.Key.Parent != NetworkTree.RootName)
                {
                    rows = rows.Where(r => r[$"{node.Item.Key.Parent}_Id"].Equals(rootId));
                }

                var dataRow = rows.FirstOrDefault(r => r["UniqueName"].Equals(objectName));
                int id = dataRow != null ? (int)dataRow[$"{table.TableName}_Id"] : -1;

                if (id == -1)
                {
                    dataRow = table.NewRow();
                    foreach (var column in node.Item)
                    {
                        var cellValue = (listObject.DataBodyRange[row, column.Index + 1] as Excel.Range).Value2;
                        dataRow[column.FieldName] = column.TransformValue(cellValue) ?? DBNull.Value;
                    }
                    if (node.Item.Key.Parent != NetworkTree.RootName)
                    {
                        dataRow[$"{node.Item.Key.Parent}_Id"] = rootId;
                    }
                    table.Rows.Add(dataRow);
                }                                       

                if (node.Children.Any())
                {
                    if (id == -1)
                    {
                        id = (int)dataRow[$"{node.Item.Key.RefObject}_Id"];
                    }
                    CreateOrUpdateRowInDataSet(row, dataSet, listObject, id, node.Children);
                }

            }
        }

        void ICommand<PreserveDataBlocksCommandArgs>.Execute(PreserveDataBlocksCommandArgs commandData)
        {
            if (commandData.Solution != null)
            {
                _blockProvider.SetLastSolutionId(commandData.Solution.Id);
            }
            _blockProvider.Save();
        }

        //private void RemoveRowInDataSet(int row, DataSet dataSet, Excel.ListObject listObject, int rootId, IEnumerable<TreeItem<IGrouping<ObjectIdentity, ColumnIdentity>>> currentItems)
        //{
        //    foreach (var node in currentItems)
        //    {
        //        var table = dataSet.Tables[node.Item.Key.RefObject];
        //        //var dataRow = table.NewRow();
        //        //foreach (var column in node.Item)
        //        //{
        //        //    var cellValue = (listObject.DataBodyRange[row, column.Index + 1] as Excel.Range).Value2;
        //        //    dataRow[column.FieldName] = cellValue;
        //        //}
        //        //if (dataRow["UniqueName"] is System.DBNull)
        //        //{
        //        //    return;
        //        //}

        //        var rows = table.AsEnumerable();

        //        if (node.Item.Key.Parent != NetworkTree.RootName)
        //        {
        //            rows = rows.Where(r => r[$"{node.Item.Key.Parent}_Id"].Equals(rootId));
        //        }

        //        var match = rows.FirstOrDefault(r => r["UniqueName"].Equals(dataRow["UniqueName"]));

        //        int id = match != null ? (int)match[$"{table.TableName}_Id"] : -1;

        //        if (id == -1)
        //        {
        //            table.Rows.Add(dataRow);
        //        }
        //        else
        //        {
        //            foreach (var column in node.Item)
        //            {
        //                if (!match[column.FieldName].Equals(dataRow[column.FieldName]))
        //                {
        //                    match[column.FieldName] = dataRow[column.FieldName];
        //                }
        //            }
        //        }

        //        if (node.Children.Any())
        //        {
        //            if (id == -1)
        //            {
        //                id = (int)dataRow[$"{node.Item.Key.RefObject}_Id"];
        //            }
        //            CreateOrUpdateRowInDataSet(row, dataSet, listObject, id, node.Children);
        //        }

        //    }
        //}

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

            protected ObjectIdentity(DataBlock sf)
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

            private Func<dynamic, dynamic> _transform = (d) => d;

            public ColumnIdentity(DataBlock sf, int index) : base(sf)
            {
                Index = index;
                int start = -1;

                var nsf = sf as NodeDataBlock;
                if (nsf != null)
                {
                    start = sf.XmlPath.LastIndexOf('@');

                    if(nsf.NodeType == NodeType.Since || nsf.NodeType == NodeType.Till)
                    {
                        _transform = (d) => d == null ? null : DateTime.FromOADate(d);
                    }
                }

                var vsf = sf as VariableDataBlock;
                if (vsf != null)
                {
                    start = sf.XmlPath.LastIndexOf('/');
                }

                FieldName = sf.XmlPath.Substring(start + 1, sf.XmlPath.Length - (start + 1));
            }

            public dynamic TransformValue(dynamic value)
            {
                return _transform?.Invoke(value);
            }

        }

        #endregion
    }
}
