using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class SelectEntityEventArgs : EventArgs
    {
        public DataBlock Fragment { get; set; }
        public CollectionDataBlock SourceCollection { get; set; }
    } 

    public interface IFragmentView : IPanes
    {
        event EventHandler<SelectEntityEventArgs> FragmentSelected;
        event EventHandler<SelectEntityEventArgs> NewFragmentRequested;
        event EventHandler<SelectEntityEventArgs> FragmentDeleted;
        event EventHandler<SelectEntityEventArgs> FragmentCopied;
        void RefreshFragmentsView(IEnumerable<BaseDataBlock> fragments);
    }
}
