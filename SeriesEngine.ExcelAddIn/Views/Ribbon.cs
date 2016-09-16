using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using SeriesEngine.ExcelAddIn.Views;
using Microsoft.Office.Core;

namespace SeriesEngine.ExcelAddIn
{
    public partial class Ribbon : IMainMenuView
    {
        public event EventHandler<PaneArgs> ShowFragmentsPane;
        public event EventHandler<PaneArgs> ShowPeriodSelectorPane;
        public event EventHandler RefreshAll;

        private PaneArgs CreatePaneArgs(object toggleButton)
        {
            return new PaneArgs
            {
                Visible = ((RibbonToggleButton)toggleButton).Checked
            };
        }

        private void toggleButtonShowPeriodSelector_Click(object sender, RibbonControlEventArgs e)
        {
            ShowPeriodSelectorPane?.Invoke(this, CreatePaneArgs(sender));
        }

        private void toggleButtonShowFragmetns_Click(object sender, RibbonControlEventArgs e)
        {
            ShowFragmentsPane?.Invoke(this, CreatePaneArgs(sender));
        }

        private void buttonRefresh_Click(object sender, RibbonControlEventArgs e)
        {
            RefreshAll?.Invoke(this, EventArgs.Empty);
        }

        public void SetFragmentsButtonState(bool isChecked)
        {
            toggleButtonShowFragmetns.Checked = isChecked;
        }

        public void SetPeriodButtonState(bool isChecked)
        {
            toggleButtonShowPeriodSelector.Checked = isChecked;
        }

    }
}
