using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IDataBlockProvider
    {
        IEnumerable<BaseDataBlock> GetDataBlocks(string filter);
        DataBlock CreateDataBlock(CollectionDataBlock source);
        DataBlock CopyDataBlock(DataBlock sourceFragment, CollectionDataBlock sourceCollection);
        void AddDataBlock(DataBlock fragment);
        void DeleteDataBlock(DataBlock fragment);

        Period GetDefaultPeriod();
        void SetDefaultPeriod(Period p);
    }   
}
