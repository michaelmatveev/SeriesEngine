using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class SelectEntityEventArgs : EventArgs
    {
        public DataBlock Block { get; set; }
        public CollectionDataBlock SourceCollection { get; set; }
    } 

    public interface IDataBlockView : IPanes
    {
        BaseDataBlock SelectedBlock { get; set; }
        event EventHandler<SelectEntityEventArgs> DataBlockSelected;
        event EventHandler<SelectEntityEventArgs> NewDataBlockRequested;
        event EventHandler<SelectEntityEventArgs> DataBlockDeleted;
        event EventHandler<SelectEntityEventArgs> DataBlockCopied;
        void RefreshDataBlockView(IEnumerable<BaseDataBlock> blocks);
    }
}
