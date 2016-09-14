using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using SeriesEngine.ExcelAddIn.Views;

namespace SeriesEngine.ExcelAddIn
{
    public partial class Ribbon : IMainMenuView
    {
        public event EventHandler ShowFragmentsPane;
        public event EventHandler ShowPeriodSelectorPane;

        private void buttonConnectSolution_Click(object sender, RibbonControlEventArgs e)
        {
            ShowFragmentsPane?.Invoke(this, EventArgs.Empty);
        }

        private void toggleButtonShowPeriodSelector_Click(object sender, RibbonControlEventArgs e)
        {
            ShowPeriodSelectorPane?.Invoke(this, EventArgs.Empty);
        }
    }
}
