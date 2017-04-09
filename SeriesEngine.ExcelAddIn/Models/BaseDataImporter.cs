using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public abstract class BaseDataImporter
    {
        protected void ImportDataForDataBlocks(int solutionId, IEnumerable<SheetDataBlock> dataBlocks, Period period)
        {
            foreach (var dataBlock in dataBlocks)
            {
                dataBlock.Import(solutionId, this);  
            }            
        }

        public abstract void ImportDataBlock(int solutionId, CollectionDataBlock fragment);
    }
}
