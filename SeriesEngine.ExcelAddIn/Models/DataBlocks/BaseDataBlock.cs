namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    public class BaseDataBlock
    {
        public string Name { get; set; }
        public BaseDataBlock Parent { get; set; }

        public BaseDataBlock()
        {
        }

        public BaseDataBlock(BaseDataBlock parent)
        {
            Parent = parent;
        }
    }
}
