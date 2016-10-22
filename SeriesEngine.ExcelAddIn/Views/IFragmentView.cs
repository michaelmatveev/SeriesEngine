using SeriesEngine.ExcelAddIn.Models.Fragments;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class SelectEntityEventArgs : EventArgs
    {
        public DataFragment Fragment { get; set; }
        public CollectionFragment SourceCollection { get; set; }
    } 

    public interface IFragmentView : IPanes
    {
        event EventHandler<SelectEntityEventArgs> FragmentSelected;
        event EventHandler<SelectEntityEventArgs> NewFragmentRequested;
        event EventHandler<SelectEntityEventArgs> FragmentDeleted;
        event EventHandler<SelectEntityEventArgs> FragmentCopied;
        void RefreshFragmentsView(IEnumerable<BaseFragment> fragments);
    }
}
