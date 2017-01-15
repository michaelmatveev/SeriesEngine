using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IDataBlockPropertiesView : IView
    {
        event EventHandler VariableBlockChanged;
        DataBlock DataBlock { get; set; }
        //IEnumerable<Network> Networks { get; set; }
        void ShowIt();
    }
}
