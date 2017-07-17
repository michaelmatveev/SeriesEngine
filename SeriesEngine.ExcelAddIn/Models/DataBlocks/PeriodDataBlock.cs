using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    public class PeriodDataBlock : DataBlock
    {
        public PeriodDataBlock(BaseDataBlock parent) : base(parent)
        {
        }

        public TimeInterval PeriodInterval { get; set; } = TimeInterval.None;
    }
}
