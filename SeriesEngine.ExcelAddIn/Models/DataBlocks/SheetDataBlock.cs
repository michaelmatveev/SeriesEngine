using SeriesEngine.Core.DataAccess;
using System;

namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    [Serializable]
    public abstract class SheetDataBlock : BaseDataBlock
    {
        public Period CustomPeriod { get; set; }

        public string Sheet { get; set; }
        public string Cell { get; set; }

        public SheetDataBlock(BaseDataBlock parent, Period defaultPeriod)
        {
            Parent = parent;
            CustomPeriod = defaultPeriod;
        }

        public abstract void Import(int solutionId, BaseDataImporter importer);
        public abstract void Export(int solutionId, BaseDataExporter exproter);
    }
}
