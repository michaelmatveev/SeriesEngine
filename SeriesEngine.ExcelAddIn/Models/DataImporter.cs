using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.Core.DataAccess;
using System.Text.RegularExpressions;

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
                
                Excel.Worksheet sheet = _workbook.Sheets[collectionDatablock.Sheet];
                sheet.Activate();
                var tableCell = sheet.get_Range(collectionDatablock.Cell);
                
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

                    var block = collectionDatablock.DataBlocks.First();
                    SetColumn(column, xmlMap, block, solution);
                }

                foreach (var f in collectionDatablock.DataBlocks.Skip(collectionDatablock.AddIndexColumn ? 0 : 1))
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
                var network = _networksProvider
                    .GetNetwork(solution.Id, collectionDatablock.NetworkName, collectionDatablock.DataBlocks, period);

                using (network.GetImportLock(collectionDatablock))
                {
                    foreach (var b in collectionDatablock.DataBlocks)
                    {
                        b.VariablePeriod = period; // TODO вычислить период в зависимости от сдвига
                    }
                    var xml = network.ConvertToXml(collectionDatablock.DataBlocks, period);
                    collectionDatablock.Xml = xml;

                    var result = xmlMap.ImportXml(xml.ToString(), true);
                    // call after data assigment
                    if (collectionDatablock.AddIndexColumn)
                    {
                        SetupIndexerColumn(listObject, collectionDatablock.Cell);
                    }

                    if (listObject.ShowHeaders)
                    {
                        listObject.HeaderRowRange.Validation.Delete();
                    }
                    
                    listObject.Range.Columns.AutoFit();                 
                }
            }
            finally
            {
                _workbook.Application.EnableEvents = true;
                _workbook.Application.DisplayAlerts = true;
            }
        }

        private static void SetupIndexerColumn(Excel.ListObject listObject, string startCell)
        {
            var column = listObject
                .ListColumns
                .Cast<Excel.ListColumn>()
                .First();
            var startRow = Regex.Match(startCell, @"\d+");
            var rowNumberFormula = $"=ROW() - {startRow}";
            column.DataBodyRange.Formula = rowNumberFormula;
            if (listObject.ShowHeaders)
            {
                column.Name = "#";
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
            var nodeType = (block as NodeDataBlock)?.NodeType ?? NodeType.Path;
            column.Range.Validation.Delete();
            if (nodeType == NodeType.Since || nodeType == NodeType.Till)
            {
                column.Range.NumberFormat = "dd.mm.yyyy";
            }
            else if(nodeType == NodeType.UniqueName)
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

        //private void ImportNodeFragment(NodeFragment fragment)
        //{
        //    Excel.Worksheet sheet = _workbook.Sheets[fragment.Sheet];
        //    sheet.get_Range(fragment.Cell).Value = fragment.Name;

        //    MockNetworkProvider netwrokProvider = new MockNetworkProvider();
        //    var network = netwrokProvider.GetNetworks(string.Empty);

        //    int i = 1;
        //    foreach(var node in (network.First() as NetworkTree).Nodes.Where(n => n.LinkedObject.ObjectModel == fragment.Model))
        //    {
        //        var cell = sheet.get_Range(fragment.Cell).Offset[i++, 0];
        //        switch (fragment.NodeValue)
        //        {
        //            case ExportNodeValue.Name: cell.Value2 = node.NodeName; break;
        //            case ExportNodeValue.Since: cell.Value2 = node.Since; break;
        //            case ExportNodeValue.Till: cell.Value2 = node.Till; break;
        //        }
        //    }
        //}

        //public void ImportFromFragments(IEnumerable<Fragment> fragments, Period period)
        //{
        //    foreach (var f in fragments)
        //    {
        //        if (f.UseCommonPeriod)
        //        {
        //            ImportFramgent(f, f.CustomPeriod);
        //        }
        //        else
        //        {
        //            ImportFramgent(f, period);
        //        }
        //    }
        //}

        //private void ImportFramgent(Fragment fragment, Period period)
        //{
        //    Excel.Worksheet sheet = _workbook.Sheets[fragment.Sheet];
        //    sheet.get_Range(fragment.Cell).Value = fragment.Name;

        //    int row = 0;
        //    int col = 0;
        //    if (fragment.IntervalsByRows)
        //    {
        //        DateTime d = GetStartDate(period.From, fragment.Interval);
        //        for (int i = 0; i < 20; i++) //TODO Replace on interation throught collection
        //        {
        //            while (d < period.Till)
        //            {
        //                row = 0;
        //                if (fragment.ShowIntervals)
        //                {
        //                    sheet.get_Range(fragment.Cell).Offset[row, i].Value2 = d;
        //                    sheet.get_Range(fragment.Cell).Offset[row, i].NumberFormat = GetFormat(fragment.Interval);
        //                    sheet.get_Range(fragment.Cell).Offset[row++, i + 1].Value2 = _random.Next(100);
        //                }
        //                else
        //                {
        //                    sheet.get_Range(fragment.Cell).Offset[row++, i].Value2 = _random.Next(100);
        //                }
        //                d = GetNextDate(d, fragment.Interval);
        //            }
        //        }
        //    }        
        //    else
        //    {
        //        for (int i = 0; i < 20; i++) //TODO Replace on interation throught collection
        //        {
        //            DateTime d = GetStartDate(period.From, fragment.Interval);
        //            while (d < period.Till)
        //            {
        //                col = 0;
        //                if (fragment.ShowIntervals)
        //                {
        //                    sheet.get_Range(fragment.Cell).Offset[i, col].Value2 = d;
        //                    sheet.get_Range(fragment.Cell).Offset[i, col].NumberFormat = GetFormat(fragment.Interval);
        //                    sheet.get_Range(fragment.Cell).Offset[i + 1, col++].Value2 = _random.Next(100);
        //                }
        //                else
        //                {
        //                    sheet.get_Range(fragment.Cell).Offset[i, col++].Value2 = _random.Next(100);
        //                }
        //                d = GetNextDate(d, fragment.Interval);
        //            }                    
        //        }
        //    }

        //}

        //private DateTime GetStartDate(DateTime from, TimeInterval interval)
        //{
        //    switch (interval)
        //    {
        //        case TimeInterval.Year:
        //            return from.FirstDayOfYear();
        //        case TimeInterval.Month:
        //            return from.FirstDayOfMonth();
        //        case TimeInterval.Week:
        //            return from.FirstDayOfWeek();
        //        case TimeInterval.Day:
        //        case TimeInterval.Hour:
        //        case TimeInterval.Minutes30:
        //            return from;
        //        default:
        //            return DateTime.MaxValue;
        //    }
        //}

        //private DateTime GetNextDate(DateTime current, TimeInterval interval)
        //{
        //    switch (interval)
        //    {
        //        case TimeInterval.Year:
        //            return current.AddYears(1);
        //        case TimeInterval.Month:
        //            return current.AddMonths(1);
        //        case TimeInterval.Week:
        //            return current.AddDays(7);
        //        case TimeInterval.Day:
        //            return current.AddDays(1);
        //        case TimeInterval.Hour:
        //            return current.AddHours(1);
        //        case TimeInterval.Minutes30:
        //            return current.AddMinutes(30);
        //        default:
        //            return DateTime.MaxValue;
        //    }
        //}

        //private string GetFormat(TimeInterval interval)
        //{
        //    switch (interval)
        //    {
        //        case TimeInterval.Year:
        //            return "YYYY";
        //        case TimeInterval.Month:
        //            return "MMMM YYYY";
        //        case TimeInterval.Week:
        //            return "dd.MM.YYYY";
        //        case TimeInterval.Day:
        //            return "dd.MM.YYYY";
        //        case TimeInterval.Hour:
        //            return "dd.MM.YYYY hh:mm";
        //        case TimeInterval.Minutes30:
        //            return "dd.MM.YYYY hh:mm";
        //        default:
        //            return string.Empty;
        //    }

        //}

    }
}
