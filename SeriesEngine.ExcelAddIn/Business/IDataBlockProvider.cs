﻿using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IDataBlockProvider
    {
        void Save();
        IEnumerable<BaseDataBlock> GetDataBlocks();
        DataBlock CreateDataBlock(CollectionDataBlock source);
        DataBlock CopyDataBlock(DataBlock sourceFragment, CollectionDataBlock sourceCollection);
        void AddDataBlock(CollectionDataBlock fragment);
        void DeleteDataBlock(BaseDataBlock fragment);

        Period GetDefaultPeriod(CollectionDataBlock datablock = null);
        void SetDefaultPeriod(Period p);

        int GetLastSolutionId();
        void SetLastSolutionId(int solutionId);

        string GetXml(string name);
        void UpdateXml(string name, string xml);
    }   
}
