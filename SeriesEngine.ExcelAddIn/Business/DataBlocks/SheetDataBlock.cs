using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Business.Export;
using SeriesEngine.ExcelAddIn.Business.Import;
using System;

namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    [Serializable]
    public abstract class SheetDataBlock : BaseDataBlock
    {
        public string Sheet { get; set; }
        public string StartCell { get; set; }
        public string EndCell { get; set; }

        public SheetDataBlock(BaseDataBlock parent)
        {
            Parent = parent;
        }

        public abstract void Import(Solution solution, BaseDataImporter importer);
        public abstract void Export(Solution solution, BaseDataExporter exproter);
    }
}
