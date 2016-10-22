using SeriesEngine.ExcelAddIn.Models.Fragments;
using System;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IFragmentPropertiesView : IView
    {
        event EventHandler FragmentChanged;
        DataFragment Fragment { get; set; }
        //IEnumerable<Network> Networks { get; set; }
        void ShowIt();
    }
}
