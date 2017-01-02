using System;
using System.Collections.Generic;
using Microsoft.Office.Tools.Ribbon;
using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.Msk1;
using Microsoft.Office.Interop.Excel;

namespace SeriesEngine.ExcelAddIn
{
    public partial class Ribbon : IMainMenuView
    {
        //public event EventHandler<PaneArgs> ShowFragmentsPane;
        public event EventHandler<PaneArgs> ShowCustomPane;
        public event EventHandler RefreshAll;
        public event EventHandler SaveAll;
        public event EventHandler<FilterArgs> FilterSelected;
        public event EventHandler<CurrentSelectionArgs> InsertNewDataBlock;

        private PaneArgs CreatePaneArgs(object toggleButton)
        {
            return new PaneArgs
            {
                Visible = ((RibbonToggleButton)toggleButton).Checked
            };
        }

        //private void toggleButtonShowFragmetns_Click(object sender, RibbonControlEventArgs e)
        //{
        //    ShowFragmentsPane?.Invoke(this, CreatePaneArgs(sender));
        //}

        private void toggleButtonShowPane_Click(object sender, RibbonControlEventArgs e)
        {
            ShowCustomPane?.Invoke(this, CreatePaneArgs(sender));
        }

        private void buttonRefresh_Click(object sender, RibbonControlEventArgs e)
        {
            RefreshAll?.Invoke(this, EventArgs.Empty);
        }

        private void buttonSave_Click(object sender, RibbonControlEventArgs e)
        {
            SaveAll?.Invoke(this, EventArgs.Empty);
        }
        
        private void buttonAddDataBlock_Click(object sender, RibbonControlEventArgs e)
        {
            var range = (Range)Globals.ThisAddIn.Application.Selection;
            var sheet = range.Parent.Name;
            var cell = range.AddressLocal.Replace("$", string.Empty);
            InsertNewDataBlock?.Invoke(this, new CurrentSelectionArgs
            {
                Sheet = sheet,
                Cell = cell, 
                Name = cell
            });
        }

        //public void SetFragmentsButtonState(bool isChecked)
        //{

        //    toggleButtonShowFragmetns.Checked = isChecked;
        //}

        public void SetPaneVisibleState(bool isChecked)
        {
            toggleButtonShowPane.Checked = isChecked;
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
