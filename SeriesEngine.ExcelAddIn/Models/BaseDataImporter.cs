using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public abstract class BaseDataImporter
    {
        protected void ImportDataForFragments(int solutionId, IEnumerable<SheetDataBlock> fragments, Period period)
        {
            foreach (var f in fragments)
            {
                f.Import(solutionId, this);  
            }            
        }

        public abstract void ImportDataBlock(int solutionId, CollectionDataBlock fragment);
    }
}
