using SeriesEngine.Core.DataAccess;
using System;

namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    [Serializable]
    public abstract class SheetDataBlock : BaseDataBlock
    {
        public string Sheet { get; set; }
        public string Cell { get; set; }

        public SheetDataBlock(BaseDataBlock parent)
        {
            Parent = parent;
        }

        public abstract void Import(int solutionId, BaseDataImporter importer);
        public abstract void Export(int solutionId, BaseDataExporter exproter);
    }
}
