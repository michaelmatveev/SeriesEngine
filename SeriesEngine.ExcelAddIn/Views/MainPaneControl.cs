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

        public MainPaneControl(PanesManager embedder) : base(embedder, "Ряды данных")
        {
            InitializeComponent();
        }

        public event EventHandler<SwitchToViewEventArgs> SwitchToTheView;

        public void SetViews(ICollection<string> viewNames)
        {
            comboBoxGoTo.DataSource = viewNames;
        }

        public void InflateControl(Control control)
        {
            panelToPlace.Controls.Clear();
            if (control != null)
            {
                panelToPlace.Controls.Add(control);
            }
        }

        private void comboBoxGoTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchToTheView?.Invoke(this, new SwitchToViewEventArgs
            {
                ViewName = comboBoxGoTo.Text
            });
        }
    }
}
