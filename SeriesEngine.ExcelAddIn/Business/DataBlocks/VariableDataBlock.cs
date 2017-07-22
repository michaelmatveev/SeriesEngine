using SeriesEngine.Core;
using SeriesEngine.ExcelAddIn.Helpers;

namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    public class VariableDataBlock : DataBlock
    {
        public Variable VariableMetamodel { get; set; }

        public VariableDataBlock(BaseDataBlock parent) : base(parent)
        {
        }

        public string VariableBlockName => VariableNameParser.GetVariableElementName(this);

    }
}
