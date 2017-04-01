using FluentDateTime;
using System;

namespace SeriesEngine.Core.DataAccess
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

        public bool Include(DateTime date)
        {
            return From <= date && Till > date;
        }

        public bool Include(DateTime start, DateTime finish)
        {
            return From <= start && Till > finish;
        }

        public bool Intersect(DateTime start, DateTime finish)
        {
            return From < finish && Till >= start;
        }

        public override string ToString()
        {
            return $"c {From.ToString("dd.MM.yyyy")} по {Till.ToString("dd.MM.yyyy")}";
        }
    }
}
