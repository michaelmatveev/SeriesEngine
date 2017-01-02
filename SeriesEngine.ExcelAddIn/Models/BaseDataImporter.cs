using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public abstract class BaseDataImporter
    {
        protected void ImportDataForFragments(IEnumerable<SheetDataBlock> fragments, Period period)
        {
            foreach (var f in fragments)
            {
                f.Import(this);  
            }            
        }

        public abstract void ImportFragment(CollectionDataBlock fragment);
    }
}
