using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Linq;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Helpers;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataImporter : BaseDataImporter,
        ICommand<ReloadAllCommandArgs>,
        ICommand<ReloadDataBlockCommandArgs>
    {
        private readonly Workbook _workbook;
        private readonly IDataBlockProvider _blockProvider;
        private readonly INetworksProvider _networksProvider;
        private Random _random = new Random();

        public DataImporter(Workbook workbook, IDataBlockProvider blockProvider, INetworksProvider networksProvider)
        {
            _workbook = workbook;
            _blockProvider = blockProvider;
            _networksProvider = networksProvider;
        }

        void ICommand<ReloadAllCommandArgs>.Execute(ReloadAllCommandArgs commandData)
        {
            using (new ActiveRangeKeeper(_workbook))
            {
                var sheetDataBlocks = _blockProvider
                    .GetDataBlocks()
                    .OfType<CollectionDataBlock>();
                
                var period = _blockProvider.GetDefaultPeriod();
                ImportDataForDataBlocks(commandData.Solution.Id, sheetDataBlocks, period);
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
                ImportDataBlock(commandData.Solution.Id, sheetDataBlocks.Single(sb => sb.Name == commandData.BlockName));
                _blockProvider.Save(); // save NetworkRevision
            }
        }

        public override void ImportDataBlock(int solutionId, CollectionDataBlock collectionDatablock)
        {
            Excel.Worksheet sheet = _workbook.Sheets[collectionDatablock.Sheet];
            sheet.Activate();
            sheet.get_Range(collectionDatablock.Cell).Select();

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

            listObject?.Delete();

            listObject = sheet.ListObjects.Add();
            listObject.Name = collectionDatablock.Name;

            var column = listObject.ListColumns
                .Cast<Excel.ListColumn>()
                .First();

            var block = collectionDatablock.DataBlocks.First();
            SetColumn(column, xmlMap, block);

            foreach (var f in collectionDatablock.DataBlocks.Skip(1))
            {
                var newColumn = listObject.ListColumns.Add();
                SetColumn(newColumn, xmlMap, f);
            }

            var blocks = collectionDatablock
                .DataBlocks
                .OfType<VariableDataBlock>()
                .Where(b => b.VariableMetamodel.IsPeriodic)
                .ToList();

            var period = collectionDatablock.PeriodType == PeriodType.Common ? _blockProvider.GetDefaultPeriod() : collectionDatablock.CustomPeriod;

            listObject.ShowHeaders = collectionDatablock.ShowHeader;
            var network = _networksProvider
                .GetNetwork(solutionId, collectionDatablock.NetworkName, blocks, period);
 
            using (network.GetImportLock(collectionDatablock))
            {
                var xml = network.ConvertToXml(collectionDatablock.DataBlocks, period);
                collectionDatablock.Xml = xml;
                var results = xmlMap.ImportXml(xml.ToString(), true);
            }
        }

        private static void SetColumn(Excel.ListColumn column, Excel.XmlMap map, DataBlock block)
        {
            var nodeType = (block as NodeDataBlock)?.NodeType ?? NodeType.Path;
            column.Name = nodeType == NodeType.UniqueName ? block.Caption + "(+)" : block.Caption;
            if (nodeType == NodeType.Since || nodeType == NodeType.Till)
            {
                column.Range.NumberFormat = "dd.mm.yyyy";
            }
            else
            {
                column.Range.NumberFormat = "@";
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
