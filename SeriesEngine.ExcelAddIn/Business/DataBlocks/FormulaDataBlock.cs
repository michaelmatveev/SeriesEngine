namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    public class FormulaDataBlock : DataBlock
    {
        public FormulaDataBlock(BaseDataBlock parent) : base(parent)
        {
        }

        public string Formula { get; set; }
    }
}
