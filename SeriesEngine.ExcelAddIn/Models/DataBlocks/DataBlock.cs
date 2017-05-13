using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Helpers;

namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    public class DataBlock : BaseDataBlock
    {
        public bool Visible { get; set; }
        public string XmlPath { get; set; }
        public string Caption { get; set; }
        public int Level { get; set; }
        public string CollectionName { get; set; }
        public string RefObject { get; set; }
        public ObjectMetamodel ObjectMetamodel { get; set; }
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

    public class NodeDataBlock : DataBlock
    {
        public NodeType NodeType;
        public string ObjectName;
        public NodeDataBlock(BaseDataBlock parent) : base(parent)
        {
        }
    }

    public class VariableDataBlock : DataBlock
    {
        public Variable VariableMetamodel { get; set; }

        public VariableDataBlock(BaseDataBlock parent) : base(parent)
        {
        }

        public string VariableBlockName => VariableNameParser.GetVariableElementName(this);

    }
}
