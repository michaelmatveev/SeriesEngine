using System;

namespace SeriesEngine.ExcelAddIn.Helpers
{
    public static class DateTimeParser
    {
        public static DateTime? Parse(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return null;
            }

            try
            {
                return DateTime.Parse(value);
            }
            catch(FormatException)
            {
            }

            try
            {
                double d = double.Parse(value);
                return DateTime.FromOADate(d);
            }
            catch (ArgumentException)
            {
            }

            return null;
        }

    }
}
