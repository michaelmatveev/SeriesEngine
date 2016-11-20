using SeriesEngine.Msk1;
using System;

namespace SeriesEngine.ExcelAddIn.Models.Fragments
{
    public class DataFragment : SheetFragment
    {
        public ObjectMetamodel ObjectMetamodel { get; set; }
        public Variable VariableMetamodel { get; set; }
        public Kind Kind { get; set; }

        public TimeInterval Interval { get; set; }
        public bool IntervalsByRows { get; set; }
        public bool IntervalsByColumns
        {
            get
            {
                return !IntervalsByRows;
            }
            set
            {
                IntervalsByRows = !value;
            }
        }
        public bool ShowIntervals { get; set; }

        public bool UseCommonPeriod { get; set; }
        public bool UseShift { get; set; }
        public int Shift { get; set; }
        public TimeInterval ShiftPeriod { get; set; }

        public DataFragment(BaseFragment parent, Period defaultPeriod) : base(parent, defaultPeriod)
        {
            IntervalsByRows = true;
            UseCommonPeriod = true;
        }

        public override void Import(BaseDataImporter importer)
        {
            throw new NotImplementedException();
        }

        public override void Export(BaseDataExporter exproter)
        {
            throw new NotImplementedException();
        }
    }
}
