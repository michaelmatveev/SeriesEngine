using FluentDateTime;
using System;

namespace SeriesEngine.Core.DataAccess
{
    [Serializable]
    public class Period
    {
        public DateTime From { get; set; }
        public DateTime Till { get; set; }

        public DateTime FromDate => From.Date;

        public DateTime TillDate => Till.Date;

        public static Period Default
        {
            get
            {
                var now = DateTime.Now;
                return new Period()
                {
                    From = now.FirstDayOfMonth().BeginningOfDay(),
                    Till = now.FirstDayOfMonth().BeginningOfDay().AddMonths(1)
                };
            }
        }


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
            return From < finish && Till > start;
        }

        public override string ToString()
        {
            return $"c {From.ToString("dd.MM.yyyy")} до {Till.ToString("dd.MM.yyyy")}";
        }
    }
}
