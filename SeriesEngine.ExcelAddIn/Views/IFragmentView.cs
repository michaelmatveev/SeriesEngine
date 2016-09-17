using SeriesEngine.ExcelAddIn.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class SelectFragmentEventArgs : EventArgs
    {
        public Fragment Fragment { get; set; }
    } 

    public interface IFragmentView : IPanes
    {
        event EventHandler<SelectFragmentEventArgs> FragmentSelected;
        void RefreshFragmentsView(IEnumerable<Fragment> fragments);
    }
}
