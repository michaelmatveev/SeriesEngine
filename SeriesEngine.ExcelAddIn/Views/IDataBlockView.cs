using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class SelectEntityEventArgs : EventArgs
    {
        public BaseDataBlock Block { get; set; }
        public CollectionDataBlock SourceCollection { get; set; }
    } 

    public interface IDataBlockView : IPanes
    {
        BaseDataBlock SelectedBlock { get; set; }
        void RefreshDataBlockView(IEnumerable<BaseDataBlock> blocks);

        event EventHandler<SelectEntityEventArgs> CollectionDataBlockSelected;
        event EventHandler<SelectEntityEventArgs> DataBlockSelected;
        event EventHandler<SelectEntityEventArgs> NewDataBlockRequested;
        event EventHandler<SelectEntityEventArgs> DataBlockDeleted;
        event EventHandler<SelectEntityEventArgs> DataBlockCopied;
    }
}
