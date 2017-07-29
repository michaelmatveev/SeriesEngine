using FluentDateTime;
using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.Core.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetStartDate(this DateTime from, TimeInterval interval)
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

        public static DateTime GetNextDate(DateTime current, TimeInterval interval)
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

        public static string GetTimeFormat(TimeInterval interval)
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
