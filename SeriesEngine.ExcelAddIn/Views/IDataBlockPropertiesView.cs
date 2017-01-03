using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IDataBlockPropertiesView : IView
    {
        event EventHandler FragmentChanged;
        CollectionDataBlock DataBlock { get; set; }
        //IEnumerable<Network> Networks { get; set; }
        void ShowIt();
    }
}
