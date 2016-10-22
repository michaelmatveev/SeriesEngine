using FluentDateTime;
using System;

namespace SeriesEngine.ExcelAddIn.Models
{
    [Serializable]
    public class Period
    {
        public DateTime From { get; set; }
        public DateTime Till { get; set; }

        public static Period Default = new Period
        {
            From = DateTime.Now.FirstDayOfMonth(),
            Till = DateTime.Now.AddMonths(1).FirstDayOfMonth()
        };

    }
}
