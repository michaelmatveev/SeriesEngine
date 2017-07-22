using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    public class DataBlock : BaseDataBlock
    {
        public bool Visible { get; set; }
        public string XmlPath { get; set; }
        public string Caption { get; set; }
        public int Level { get; set; }
        public string RefObject { get; set; }
        public ObjectMetamodel ObjectMetamodel { get; set; }//TODO remove it
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
        public Period VariablePeriod { get; set; }

        public DataBlock(BaseDataBlock parent) : base(parent)
        {
            Parent = parent;
            IntervalsByRows = true;
            UseCommonPeriod = true;
        }

    }
}
