﻿using System;
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

        public NamedCollection SourceCollection { get; set; }
       
        public TimeInterval Interval { get; set; }
        public bool IntervalsByRows { get; set; }
        public bool ShowIntervals { get; set; }

        public bool UseCommonPeriod { get; set; }
        public bool UseShift { get; set; }
        public int Shift { get; set; }
        public TimeInterval ShiftPeriod { get; set; }

        public Period CustomPeriod { get; set; }

    }
}
