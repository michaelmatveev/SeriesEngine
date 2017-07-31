using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.Core.DataAccess;
using System.Text.RegularExpressions;
using SeriesEngine.Core.Helpers;
using SeriesEngine.Core;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataImporter : BaseDataImporter,
        ICommand<ReloadAllCommandArgs>,
        ICommand<ReloadDataBlockCommandArgs>
    {
        private readonly Workbook _workbook;
        private readonly IDataBlockProvider _blockProvider;
        private readonly INetworksProvider _networksProvider;
        private readonly IObjectCache _objectCache;

        public DataImporter(Workbook workbook, IDataBlockProvider blockProvider, INetworksProvider networksProvider, IObjectCache objectCache)
        {
            _workbook = workbook;
            _blockProvider = blockProvider;
            _networksProvider = networksProvider;
            _objectCache = objectCache;
        }

        void ICommand<ReloadAllCommandArgs>.Execute(ReloadAllCommandArgs commandData)
        {
            using (new ActiveRangeKeeper(_workbook))
            {
                var sheetDataBlocks = _blockProvider
                    .GetDataBlocks()
                    .OfType<CollectionDataBlock>();
                
                var period = _blockProvider.GetDefaultPeriod();
                ImportDataForDataBlocks(commandData.Solution, sheetDataBlocks, period);
                _blockProvider.Save(); // save NetworkRevision
            }
        }

        void ICommand<ReloadDataBlockCommandArgs>.Execute(ReloadDataBlockCommandArgs commandData)
        {
            using (new ActiveRangeKeeper(_workbook))
            {
                var sheetDataBlocks = _blockProvider
                    .GetDataBlocks()
                    .OfType<CollectionDataBlock>();

                var period = _blockProvider.GetDefaultPeriod();
                ImportDataBlock(commandData.Solution, sheetDataBlocks.Single(sb => sb.Name == commandData.BlockName));
                _blockProvider.Save(); // save NetworkRevision
            }
        }

        public override void ImportDataBlock(Solution solution, CollectionDataBlock collectionDatablock)
        {
            if(!_workbook.Worksheets.Cast<Excel.Worksheet>().Any(w => w.Name == collectionDatablock.Sheet))
            {
                return; // worksheet has been deleted before
            }            

            try
            {
                _workbook.Application.EnableEvents = false;
                _workbook.Application.DisplayAlerts = false;

                if (collectionDatablock.Interval == TimeInterval.None)
                {
                    ImportIntoXmlMap(solution, collectionDatablock);
                }
                else
                {
                    ImportIntoTable(solution, collectionDatablock);
                }
            }
            finally
            {
                _workbook.Application.EnableEvents = true;
                _workbook.Application.DisplayAlerts = true;
            }
        }

        private void ClearTable(Excel.Worksheet sheet, CollectionDataBlock collectionDataBlock)
        {
            //if(string.IsNullOrEmpty(collectionDataBlock.EndCell))
            //{
            //    var table = sheet.get_Range(collectionDataBlock.StartCell);
            //    table.Clear();
            //}
            var listObject = sheet
                .ListObjects
                .Cast<Excel.ListObject>()
                .SingleOrDefault(l => l.Name == collectionDataBlock.Name);
            if(listObject != null)
            {
                //listObject.Range.Clear();
                listObject.Delete();
            }
        }

        private void ImportIntoTable(Solution solution, CollectionDataBlock collectionDataBlock)
        {
            Excel.Worksheet sheet = _workbook.Sheets[collectionDataBlock.Sheet];
            sheet.Activate();

            ClearTable(sheet, collectionDataBlock);

            var nameItem = _workbook.Names.OfType<Excel.Name>().SingleOrDefault(n => n.Name == collectionDataBlock.Name);
            nameItem?.Delete();

            var period = _blockProvider.GetDefaultPeriod(collectionDataBlock);

            var networkTree = _networksProvider
                .GetNetwork(solution, collectionDataBlock.NetworkName, collectionDataBlock.DataBlocks, period);

            var model = ModelsDescription.All.Single(m => m.Name == solution.ModelName);
            foreach (var b in collectionDataBlock.DataBlocks.OfType<VariableDataBlock>())
            {
                b.VariablePeriod = new Period
                {
                    From = period.From.AddMonths(b.Shift),
                    Till = period.Till.AddMonths(b.Shift)
                };
                b.ObjectMetamodel = model.ObjectModels.Single(m => m.Name == b.RefObject);
                b.VariableMetamodel = b.ObjectMetamodel.Variables.Single(m => m.Name == b.VariableBlockName);
            }

            var groups = networkTree.ConvertToGroups(collectionDataBlock.DataBlocks, period, collectionDataBlock.CustomPath);
            var d = period.From.GetStartDate(collectionDataBlock.Interval);
            var row = collectionDataBlock.ShowHeader ? 1 : 0;
            Excel.Range valueCells = null;
            Excel.Range valueStartCell = null;
            Excel.Range valueEndCell = null;
            while (d < period.Till)
            {
                var dateCell = sheet.get_Range(collectionDataBlock.StartCell).Offset[row, 0];
                dateCell.Value2 = d;
                dateCell.NumberFormat = DateTimeHelper.GetTimeFormat(collectionDataBlock.Interval);
                var column = 1;
                foreach (var varGroup in groups)
                {
                    var countOfObjects = varGroup.ObjectsToScan.Count;
                    if (collectionDataBlock.ShowHeader)
                    {
                        var captionStartCell = sheet.get_Range(collectionDataBlock.StartCell).Offset[0, column];
                        var captionEndCell = sheet.get_Range(collectionDataBlock.StartCell).Offset[0, column + countOfObjects - 1];
                        var captionCells = sheet.get_Range(captionStartCell, captionEndCell);
                        var captionArray = new object[1, countOfObjects];
                        for(int i = 0; i < countOfObjects; i++)
                        {
                            captionArray[0, i] = varGroup.ObjectsToScan[i].Name;
                        }
                        captionCells.Value2 = captionArray;
                    }

                    var values = varGroup.GetSlice(d);
                    valueStartCell = sheet.get_Range(collectionDataBlock.StartCell).Offset[row, column];
                    valueEndCell = sheet.get_Range(collectionDataBlock.StartCell).Offset[row, column + countOfObjects - 1];
                    valueCells = sheet.get_Range(valueStartCell, valueEndCell);
                    var valueArray = new object[1, countOfObjects];
                    for(int i = 0; i < countOfObjects; i++)
                    {
                        valueArray[0, i] = values[i];
                    }
                    valueCells.Value2 = valueArray;
                    column++;
                }
                d = DateTimeHelper.GetNextDate(d, collectionDataBlock.Interval);
                row++;
            }

            if (valueCells != null)
            {
                collectionDataBlock.EndCell = valueEndCell.AddressLocal;
                var tableCell = sheet.get_Range(collectionDataBlock.StartCell, collectionDataBlock.EndCell);

                var listObject = sheet
                    .ListObjects
                    .Cast<Excel.ListObject>()
                    .SingleOrDefault(l => l.Name == collectionDataBlock.Name);

                if (listObject == null)
                {
                    listObject = sheet.ListObjects.Add(
                        SourceType: Excel.XlListObjectSourceType.xlSrcRange,
                        Source: tableCell, 
                        Destination: tableCell, 
                        XlListObjectHasHeaders: collectionDataBlock.ShowHeader ? Excel.XlYesNoGuess.xlYes : Excel.XlYesNoGuess.xlNo);
                    listObject.Name = collectionDataBlock.Name;
                }
            }
        }

        #region ImportIntoXmlMap

        private void ImportIntoXmlMap(Solution solution, CollectionDataBlock collectionDatablock)
        {
            Excel.Worksheet sheet = _workbook.Sheets[collectionDatablock.Sheet];
            sheet.Activate();
            var tableCell = sheet.get_Range(collectionDatablock.StartCell);

            var xmlMap = _workbook
                .XmlMaps
                .Cast<Excel.XmlMap>()
                .SingleOrDefault(m => m.Name == collectionDatablock.Name);

            xmlMap?.Delete();

            xmlMap = _workbook
                .XmlMaps
                .Add(collectionDatablock.GetSchema(), NetworkTree.RootName);
            xmlMap.Name = collectionDatablock.Name;

            var listObject = sheet
                .ListObjects
                .Cast<Excel.ListObject>()
                .SingleOrDefault(l => l.Name == collectionDatablock.Name);

            if (listObject == null)
            {
                listObject = sheet.ListObjects.Add(SourceType: Excel.XlListObjectSourceType.xlSrcRange, Source: tableCell, Destination: tableCell, XlListObjectHasHeaders: Excel.XlYesNoGuess.xlNo);
                listObject.Name = collectionDatablock.Name;
                // contains one column
            }

            foreach (var column in listObject
                    .ListColumns
                    .Cast<Excel.ListColumn>()
                    .Skip(1))
            {
                column.Delete();
            }

            if (!collectionDatablock.AddIndexColumn)
            {
                var column = listObject
                    .ListColumns
                    .Cast<Excel.ListColumn>()
                    .First();

                var block = collectionDatablock.DataBlocks.First(db => db.Visible);
                if (listObject.ShowHeaders)
                {
                    column.Name = GetColumnCaption(block);
                }
                SetColumn(column, xmlMap, block, solution);
            }

            foreach (var f in collectionDatablock.DataBlocks.Where(db => db.Visible).Skip(collectionDatablock.AddIndexColumn ? 0 : 1))
            {
                var column = listObject.ListColumns.Add();
                if (listObject.ShowHeaders)
                {
                    column.Name = GetColumnCaption(f);
                }
                SetColumn(column, xmlMap, f, solution);
            }

            var period = _blockProvider.GetDefaultPeriod(collectionDatablock);

            listObject.ShowHeaders = collectionDatablock.ShowHeader;
            var networkTree = _networksProvider
                .GetNetwork(solution, collectionDatablock.NetworkName, collectionDatablock.DataBlocks, period);

            foreach (var b in collectionDatablock.DataBlocks)
            {
                b.VariablePeriod = new Period
                {
                    From = period.From.AddMonths(b.Shift),
                    Till = period.Till.AddMonths(b.Shift)
                };
                //b.VariablePeriod = period; // TODO вычислить период в зависимости от сдвига
            }
            var xml = networkTree.ConvertToXml(collectionDatablock.DataBlocks, period, collectionDatablock.CustomPath);
            collectionDatablock.Xml = xml;

            var result = xmlMap.ImportXml(xml.ToString(), true);

            // call after data assigment
            if (collectionDatablock.AddIndexColumn)
            {
                SetupIndexerColumn(listObject, collectionDatablock.StartCell);
            }

            var index = collectionDatablock.AddIndexColumn ? 2 : 1;
            foreach (var db in collectionDatablock.DataBlocks.Where(d => d.Visible))
            {
                var fdb = db as FormulaDataBlock;
                if (fdb != null)
                {
                    SetupFormulaColumn(listObject, index, fdb);
                }
                index++;
            }

            if (listObject.ShowHeaders)
            {
                listObject.HeaderRowRange.Validation.Delete();
            }

            if (collectionDatablock.AutoFitColumns)
            {
                listObject.Range.Columns.AutoFit();
            }

        }

        private static void SetupIndexerColumn(Excel.ListObject listObject, string startCell)
        {
            var column = listObject
                .ListColumns
                .Cast<Excel.ListColumn>()
                .First();

            if (column.DataBodyRange != null)
            {
                var startRow = Regex.Match(startCell, @"\d+");
                var rowNumberFormula = $"=ROW() - {startRow}";
                column.DataBodyRange.Formula = rowNumberFormula;
            }
            if (listObject.ShowHeaders)
            {
                column.Name = "#";
            }
        }

        private static void SetupFormulaColumn(Excel.ListObject listObject, int index, FormulaDataBlock fdb)
        {
            var column = listObject.ListColumns.Cast<Excel.ListColumn>().Single(c => c.Index == index);// Skip(index).First();
            if (column.DataBodyRange != null)
            {
                column.DataBodyRange.Formula = fdb.Formula;
            }
            if (listObject.ShowHeaders)
            {
                column.Name = fdb.Caption;
            }            
        }

        private string GetColumnCaption(DataBlock block)
        {
            if (block is NodeDataBlock)
            {
                var nodeType = (block as NodeDataBlock)?.NodeType ?? NodeType.Path;
                return nodeType == NodeType.UniqueName ? block.Caption + "(+)" : block.Caption;
            }
            else
            {
                return block.Caption;
            }
        }

        private void SetColumn(Excel.ListColumn column, Excel.XmlMap map, DataBlock block, Solution solution)
        {
            if (block is FormulaDataBlock)
            {
            }
            else if(block is PeriodDataBlock)
            {
                column.Range.NumberFormat = "dd.mm.yyyy";
                column.XPath.SetValue(map, block.XmlPath);
            }
            else
            {
                var nodeType = (block as NodeDataBlock)?.NodeType ?? NodeType.Path;
                column.Range.Validation.Delete();
                if (nodeType == NodeType.Since || nodeType == NodeType.Till)
                {
                    column.Range.NumberFormat = "dd.mm.yyyy";
                }
                else if (nodeType == NodeType.UniqueName)
                {
                    var formula = _objectCache.GetObjectsOfType(solution, block.RefObject);
                    if (!string.IsNullOrEmpty(formula))
                    {
                        column.Range.Validation.Delete();
                        column.Range.Validation.Add(Excel.XlDVType.xlValidateList, Excel.XlDVAlertStyle.xlValidAlertInformation, Excel.XlFormatConditionOperator.xlBetween, formula);
                        column.Range.Validation.ShowError = false;
                        column.Range.Validation.ShowInput = false;
                        column.Range.Validation.IgnoreBlank = true;
                    }
                }
                else
                {
                    column.Range.NumberFormat = "General";//"@";
                }
                column.XPath.SetValue(map, block.XmlPath);
            }
        }

        #endregion
    }
}