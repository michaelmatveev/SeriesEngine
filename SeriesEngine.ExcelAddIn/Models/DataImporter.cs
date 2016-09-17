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
        public DataImporter(Workbook workbook)
        {
            _workbook = workbook;
        }

        public void ImportFromFragments(IEnumerable<Fragment> fragments, Period period)
        {
            foreach(var f in fragments)
            {
                if (f.UseCustomPeriod)
                {
                    ImportFramgent(f, f.CustomPeriod);
                }
                else
                {
                    ImportFramgent(f, period);
                }
            }
        }
        
        private void ImportFramgent(Fragment fragment, Period period)
        {
            Excel.Worksheet sheet = _workbook.Sheets["Лист1"];
            sheet.get_Range(fragment.Cell).Value = fragment.Name;
            DateTime d = GetStartDate(period.From, fragment.Interval);
            int i = 1;
            var random = new Random();
            while (d < period.Till)
            {
                sheet.get_Range(fragment.Cell).Offset[i, 0].Value2 = d;//.ToOADate();
                sheet.get_Range(fragment.Cell).Offset[i, 0].NumberFormat = GetFormat(fragment.Interval);
                sheet.get_Range(fragment.Cell).Offset[i++, 1].Value2 = random.Next(100);
                d = GetNextDate(d, fragment.Interval);
            }             
        }
        
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
