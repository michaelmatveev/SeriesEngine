using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using FluentDateTime;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataImporter : IDataImporter
    {
        private Workbook _workbook;
        private Random _random = new Random();

        public DataImporter(Workbook workbook)
        {
            _workbook = workbook;
        }

        public void ImportFromFragments(IEnumerable<DataFragment> fragments, Period period)
        {
            foreach (var f in fragments)
            {
                if(f is NodeFragment)
                {
                    ImportNodeFragment((NodeFragment)f);
                }
            }
        }

        private void ImportNodeFragment(NodeFragment fragment)
        {
            Excel.Worksheet sheet = _workbook.Sheets[fragment.Sheet];
            sheet.get_Range(fragment.Cell).Value = fragment.Name;

            MockNetworkProvider netwrokProvider = new MockNetworkProvider();
            var network = netwrokProvider.GetNetworks(string.Empty);

            int i = 1;
            foreach(var node in (network.First() as NetworkTree).Nodes.Where(n => n.LinkedObject.ObjectModel == fragment.Model))
            {
                var cell = sheet.get_Range(fragment.Cell).Offset[i++, 0];
                switch (fragment.NodeValue)
                {
                    case ExportNodeValue.Name: cell.Value2 = node.NodeName; break;
                    case ExportNodeValue.Since: cell.Value2 = node.Since; break;
                    case ExportNodeValue.Till: cell.Value2 = node.Till; break;
                }
            }
        }

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

        private DateTime GetStartDate(DateTime from, TimeInterval interval)
        {
            switch (interval)
            {
                case TimeInterval.Year:
                    return from.FirstDayOfYear();
                case TimeInterval.Month:
                    return from.FirstDayOfMonth();
                case TimeInterval.Week:
                    return from.FirstDayOfWeek();
                case TimeInterval.Day:
                case TimeInterval.Hour:
                case TimeInterval.Minutes30:
                    return from;
                default:
                    return DateTime.MaxValue;
            }
        }

        private DateTime GetNextDate(DateTime current, TimeInterval interval)
        {
            switch (interval)
            {
                case TimeInterval.Year:
                    return current.AddYears(1);
                case TimeInterval.Month:
                    return current.AddMonths(1);
                case TimeInterval.Week:
                    return current.AddDays(7);
                case TimeInterval.Day:
                    return current.AddDays(1);
                case TimeInterval.Hour:
                    return current.AddHours(1);
                case TimeInterval.Minutes30:
                    return current.AddMinutes(30);
                default:
                    return DateTime.MaxValue;
            }
        }

        private string GetFormat(TimeInterval interval)
        {
            switch (interval)
            {
                case TimeInterval.Year:
                    return "YYYY";
                case TimeInterval.Month:
                    return "MMMM YYYY";
                case TimeInterval.Week:
                    return "dd.MM.YYYY";
                case TimeInterval.Day:
                    return "dd.MM.YYYY";
                case TimeInterval.Hour:
                    return "dd.MM.YYYY hh:mm";
                case TimeInterval.Minutes30:
                    return "dd.MM.YYYY hh:mm";
                default:
                    return string.Empty;
            }

        }

    }
}
