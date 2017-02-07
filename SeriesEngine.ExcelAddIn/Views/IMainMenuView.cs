using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class PaneArgs: EventArgs
    {
        public bool Visible { get; set; }
    }
    
    public class FilterArgs: EventArgs
    {
        public Network SelectedNetwork { get; set; }
    }

    public class CurrentSelectionArgs: EventArgs
    {
        public string Cell { get; set; }
        public string Name { get; internal set; }
        public string Sheet { get; set; }
    } 

    public interface IMainMenuView : IView
    {
        event EventHandler<PaneArgs> ShowCustomPane;
        event EventHandler RefreshAll;
        event EventHandler SaveAll;
        event EventHandler<FilterArgs> FilterSelected;
        event EventHandler<CurrentSelectionArgs> InsertNewDataBlock;
        event EventHandler<CurrentSelectionArgs> InsertSampleBlock;
        event EventHandler Connect;

        //void InitializeFilters(IEnumerable<Network> networks);
        void SetButtonToggleState(bool isVisible);
        void SetTabVisibleState(bool isVisible);
    }
}
