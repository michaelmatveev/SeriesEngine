using SeriesEngine.ExcelAddIn.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class MainPaneControl : PaneControl, IMainPane
    {
        public MainPaneControl()
        {
            InitializeComponent();
        }

        public MainPaneControl(PanesManager embedder) : base(embedder, ViewNames.MainPaneViewName)
        {
            InitializeComponent();
        }

        public event EventHandler<SwitchToViewEventArgs> SwitchToTheView;
        private bool FireSwitchToTheView = false;

        public void SetViews(ICollection<string> viewNames, string viewToOpen)
        {
            FireSwitchToTheView = false;
            comboBoxGoTo.DataSource = viewNames;
            FireSwitchToTheView = true;
            comboBoxGoTo.SelectedItem = viewToOpen;
        }

        public void InflateControl(Control control)
        {
            panelToPlace.Controls.Clear();
            if (control != null)
            {
                control.Dock = DockStyle.Fill;
                panelToPlace.Controls.Add(control);
            }
        }

        private void comboBoxGoTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FireSwitchToTheView)
            {
                SwitchToTheView?.Invoke(this, new SwitchToViewEventArgs
                {
                    ViewName = comboBoxGoTo.Text
                });
            }
        }
    }
}
