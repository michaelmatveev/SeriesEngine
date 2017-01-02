using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IFragmentPropertiesView : IView
    {
        event EventHandler FragmentChanged;
        DataBlock Fragment { get; set; }
        //IEnumerable<Network> Networks { get; set; }
        void ShowIt();
    }
}
