using SeriesEngine.ExcelAddIn.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class SelectEntityEventArgs : EventArgs
    {
        public Fragment Fragment { get; set; }
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
