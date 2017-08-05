using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Business.Import
{
    public abstract class BaseDataImporter
    {
        protected void ImportDataForDataBlocks(Solution solution, IEnumerable<SheetDataBlock> dataBlocks, Period period)
        {
            foreach (var dataBlock in dataBlocks)
            {
                dataBlock.Import(solution, this);  
            }            
        }

        public abstract void ImportDataBlock(Solution solution, CollectionDataBlock fragment);
    }
}
