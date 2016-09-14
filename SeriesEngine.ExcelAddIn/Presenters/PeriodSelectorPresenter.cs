using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class PeriodSelectorPresenter : Presenter<IPeriodView>
    {
        public PeriodSelectorPresenter(IPeriodView view, IController controller) : base(view, controller)
        {
        }

        public void ShowPeriods()
        {
            View.ShowIt();
        }
    }
}
