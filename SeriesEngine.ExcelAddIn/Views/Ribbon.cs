using System;
using Microsoft.Office.Tools.Ribbon;
using SeriesEngine.ExcelAddIn.Views;
using Microsoft.Office.Interop.Excel;

namespace SeriesEngine.ExcelAddIn
{
    public partial class Ribbon : IMainMenuView
    {
        public event EventHandler<PaneArgs> ShowCustomPane;
        public event EventHandler RefreshAll;
        public event EventHandler SaveAll;
        public event EventHandler<FilterArgs> FilterSelected;
        public event EventHandler InsertNewDataBlock;
        public event EventHandler InsertSampleBlock;
        public event EventHandler RenameObject;
        public event EventHandler DeleteObject;
        public event EventHandler Connect;
        public event EventHandler Disconnect;

        private PaneArgs CreatePaneArgs(object toggleButton)
        {
            return new PaneArgs
            {
                Visible = ((RibbonToggleButton)toggleButton).Checked
            };
        }

        private void toggleButtonShowPane_Click(object sender, RibbonControlEventArgs e)
        {
            //var clicks = Observable.FromEventPattern<PaneArgs>(this, "ShowCustomPane");
            //clicks.Where(c => c)
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
            InsertNewDataBlock?.Invoke(this, EventArgs.Empty); 
        }

        private void buttonSample_Click(object sender, RibbonControlEventArgs e)
        {
            InsertSampleBlock?.Invoke(this, EventArgs.Empty);
        }

        public void SetButtonToggleState(bool isChecked)
        {
            toggleButtonShowPane.Checked = isChecked;
        }

        public void SetTabVisibleState(bool isVisible)
        {
            tabCustom.Visible = isVisible;
        }

        private void buttonSolution_Click(object sender, RibbonControlEventArgs e)
        {
            Connect?.Invoke(this, EventArgs.Empty);
        }

        private void buttonDisconnect_Click(object sender, RibbonControlEventArgs e)
        {
            Disconnect?.Invoke(this, EventArgs.Empty);
        }

        private void buttonRenameObject_Click(object sender, RibbonControlEventArgs e)
        {
            RenameObject?.Invoke(this, EventArgs.Empty);
        }

        //public void InitializeFilters(IEnumerable<Network> networks)
        //{
        //    foreach (var network in networks)
        //    {
        //        var item = Factory.CreateRibbonButton();
        //        item.Click += (s, e) =>
        //        {
        //            FilterSelected?.Invoke(this, new FilterArgs
        //            {
        //                SelectedNetwork = network
        //            });
        //        };
        //        item.Label = network.Name;
        //        menuFilter.Items.Add(item);
        //    }
        //}

    }
}
