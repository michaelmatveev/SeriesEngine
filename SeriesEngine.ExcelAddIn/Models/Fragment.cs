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

    public enum Kind
    {
        MostAccurate,
        Plan,
        Fact,
    }

    public class Fragment : BaseFragment
    {
        public string Sheet { get; set; }
        public string Cell { get; set; }

        public ObjectMetamodel ObjectMetamodel { get; set; }
        public Variable VariableMetamodel { get; set; }
        public Kind Kind { get; set; }

        public TimeInterval Interval { get; set; }
        public bool IntervalsByRows { get; set; }
        public bool ShowIntervals { get; set; }

        public bool UseCommonPeriod { get; set; }
        public bool UseShift { get; set; }
        public int Shift { get; set; }
        public TimeInterval ShiftPeriod { get; set; }

        public Period CustomPeriod { get; set; }

        public Fragment(BaseFragment parent, Period defaultPeriod)
        {
            Parent = parent;
            IntervalsByRows = true;
            UseCommonPeriod = true;
            CustomPeriod = defaultPeriod; 
        }

    }
}
