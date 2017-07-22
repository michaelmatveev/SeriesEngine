namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    public class NodeDataBlock : DataBlock
    {
        public NodeType NodeType;
        public string ObjectName;
        public NodeDataBlock(BaseDataBlock parent) : base(parent)
        {
        }
    }
}
