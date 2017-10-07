using Microsoft.Office.Tools.Excel;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.Core.Helpers;
using SeriesEngine.ExcelAddIn.Business.Trees;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace SeriesEngine.ExcelAddIn.Business.Export
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
            var period = _blockProvider.GetDefaultPeriod(collectionDatablock);
            collectionDatablock.SetupPeriodForNestedBlocks(solution, period);

            if (collectionDatablock.Interval == TimeInterval.None)
            {
                ExportFromXmlMap(solution, collectionDatablock);
            }
            else
            {
                ExportFromTable(solution, collectionDatablock);
            }
        }

        private void ExportFromTable(Solution solution, CollectionDataBlock collectionDataBlock)
        {
            Excel.Worksheet sheet = _workbook.Sheets[collectionDataBlock.Sheet];
            var period = _blockProvider.GetDefaultPeriod(collectionDataBlock);

            var networkTree = _networksProvider.GetNetwork(solution, collectionDataBlock);
                //.GetNetwork(solution, collectionDataBlock.NetworkName, collectionDataBlock.DataBlocks, period);
                    
            var groups = networkTree.ConvertToGroups(collectionDataBlock.DataBlocks, period, collectionDataBlock.CustomPath).OfType<VariableGroup>();

            var valuesToUpdate = new List<IStateObject>();
            var d = period.From.GetStartDate(collectionDataBlock.Interval);
            var row = collectionDataBlock.ShowHeader ? 1 : 0;
            while (d < period.Till)
            {
                var column = collectionDataBlock.AddIndexColumn ? 2 : 1;
                foreach (var varGroup in groups)
                {
                    var countOfObjects = varGroup.ObjectsToScan.Count;
                    var values = varGroup.GetSlice(d);
                    var valueStartCell = sheet.get_Range(collectionDataBlock.StartCell).Offset[row, column];
                    for (int i = 0; i < countOfObjects; i++)
                    {
                        var currentValue = valueStartCell.Offset[0, i].Value2;
                        var storedValue = values[i];
                        if ((currentValue != null && storedValue == null) || (storedValue != null && !storedValue.Equals(currentValue as object)))
                        {
                            var variableEntity = CreateVariable(varGroup.Variable, currentValue, d, varGroup.ObjectsToScan[i]);
                            valuesToUpdate.Add(variableEntity);
                        }
                    }
                    column++;
                }
                row++;
                d = DateTimeHelper.GetNextDate(d, collectionDataBlock.Interval);
            }
            networkTree.Update(valuesToUpdate);
        }
        
        public IStateObject CreateVariable(Variable modelVariable, object value, DateTime period, NamedObject parent)
        {
            var varObject = (PeriodVariable)Activator.CreateInstance(modelVariable.EntityType);
            varObject.ObjectId = parent.Id;
            varObject.State = ObjectState.Added;
            varObject.Value = value;
            varObject.Date = period;
            return varObject;
        }

        private void ExportFromXmlMap(Solution solution, CollectionDataBlock collectionDataBlock)
        {
            var period = _blockProvider.GetDefaultPeriod(collectionDataBlock);

            var sourceNetworkTree = _networksProvider.GetNetwork(solution, collectionDataBlock);
                //.GetNetwork(solution, collectionDataBlock.NetworkName, collectionDataBlock.DataBlocks, period);

            Excel.Worksheet sheet = _workbook.Sheets[collectionDataBlock.Sheet];
            var listObject = sheet.ListObjects.Cast<Excel.ListObject>().SingleOrDefault(l => l.Name == collectionDataBlock.Name);

            var schema = collectionDataBlock.GetSchema();
            var sr = new StringReader(schema);

            var dsChanged = new DataSet();
            dsChanged.ReadXmlSchema(sr);

            if (listObject.DataBodyRange != null)
            {
                var tree = collectionDataBlock
                    .DataBlocks
                    .Where(d => d.Visible) // исключаем невидимые
                    .Select((f, i) => new ColumnIdentity(f, i + (collectionDataBlock.AddIndexColumn ? 1 : 0))) // считаем номер колонки для всех видимые
                    .Where(ci => !(ci.DataBlock is FormulaDataBlock)) // исключаем формулы поскольку результыты вычислений не сохраняются в БД
                    .GroupBy(ci => new ObjectIdentity(ci.RefObject, ci.Parent))
                    .GenerateTree(n => n.Key.RefObject, p => p.Key.Parent, NetworkTree.RootName);

                for (int row = 1; row <= listObject.DataBodyRange.Rows.Count; row++)
                {
                    CreateOrUpdateRowInDataSet(row, dsChanged, listObject, 0, tree);
                }
            }

            var source = new XDocument(collectionDataBlock.Xml ?? sourceNetworkTree.ConvertToXml(collectionDataBlock.DataBlocks, period, collectionDataBlock.CustomPath));
            var target = XDocument.Parse(dsChanged.GetXml());
            sourceNetworkTree.LoadFromXml(source, target, period);
        }

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
            
            public DataBlock DataBlock { get; private set; }

            public ObjectIdentity(string refObjectName, string parentName)
            {
                RefObject = refObjectName;
                Parent = parentName;
            }

            protected ObjectIdentity(DataBlock sf)
            {
                RefObject = sf.RefObject;
                if (!string.IsNullOrEmpty(sf.XmlPath))
                {
                    Parent = sf.XmlPath.Split('/').Reverse().Skip(2).First();
                }
                DataBlock = sf;
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
                    if(vsf.VariableMetamodel.ValueType == typeof(DateTime))
                    {
                        _transform = (d) => d == null ? null : DateTime.FromOADate(d);
                    }
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
