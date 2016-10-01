﻿using SeriesEngine.ExcelAddIn.Models;
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
        event EventHandler<PaneArgs> ShowFragmentsPane;
        event EventHandler<PaneArgs> ShowPeriodSelectorPane;
        event EventHandler RefreshAll;
        event EventHandler<FilterArgs> FilterSelected;

        void InitializeFilters(IEnumerable<Network> networks);
        void SetFragmentsButtonState(bool isChecked);
        void SetPeriodButtonState(bool isChecked);
    }
}