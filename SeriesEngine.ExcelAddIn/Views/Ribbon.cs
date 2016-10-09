using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using SeriesEngine.ExcelAddIn.Views;
using Microsoft.Office.Core;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn
{
    public partial class Ribbon : IMainMenuView
    {
        public event EventHandler<PaneArgs> ShowFragmentsPane;
        public event EventHandler<PaneArgs> ShowPeriodSelectorPane;
        public event EventHandler RefreshAll;
        public event EventHandler SaveAll;
        public event EventHandler<FilterArgs> FilterSelected;

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

        private void buttonSave_Click(object sender, RibbonControlEventArgs e)
        {
            SaveAll?.Invoke(this, EventArgs.Empty);
        }

        public void SetFragmentsButtonState(bool isChecked)
        {
            toggleButtonShowFragmetns.Checked = isChecked;
        }

        public void SetPeriodButtonState(bool isChecked)
        {
            toggleButtonShowPeriodSelector.Checked = isChecked;
        }


        public void InitializeFilters(IEnumerable<Network> networks)
        {
            foreach (var network in networks)
            {
                var item = Factory.CreateRibbonButton();
                item.Click += (s, e) =>
                {
                    FilterSelected?.Invoke(this, new FilterArgs
                    {
                        SelectedNetwork = network
                    });
                };
                item.Label = network.Name;
                menuFilter.Items.Add(item);
            }
        }


    }
}
