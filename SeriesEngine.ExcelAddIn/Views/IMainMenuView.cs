using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IMainMenuView : IView
    {
        event EventHandler ShowFragmentsPane;
        event EventHandler ShowPeriodSelectorPane;
    }
}
