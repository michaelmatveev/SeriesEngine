﻿using SeriesEngine.ExcelAddIn.Models;
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

    public interface IMainMenuView : IView
    {
        event EventHandler<PaneArgs> ShowCustomPane;
        event EventHandler RefreshAll;
        event EventHandler SaveAll;
        event EventHandler<FilterArgs> FilterSelected;
        event EventHandler InsertNewDataBlock;
        event EventHandler InsertSampleBlock;
        event EventHandler Connect;
        event EventHandler Disconnect;
        event EventHandler RenameObject;
        event EventHandler DeleteObject;

        //void InitializeFilters(IEnumerable<Network> networks);
        void SetButtonToggleState(bool isVisible);
        void SetTabVisibleState(bool isVisible);
    }
}
