using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public enum TimeInterval
    {
        Year,
        Month,
        Week,
        Day,
        Hour,
        Minutes30
    }

    public class Fragment
    {
        public string Name { get; set; }
        public string Sheet { get; set; }
        public string Cell { get; set; }
        public TimeInterval Interval { get; set; }
        public bool UseCustomPeriod { get; set; }
        public Period CustomPeriod { get; set; }
    }
}
