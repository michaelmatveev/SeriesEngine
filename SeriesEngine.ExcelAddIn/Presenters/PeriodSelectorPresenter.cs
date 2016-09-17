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
            View.PaneClosed += (s, e) => Controller.GetInstance<MainMenuPresenter>().SetPeriodButton(false);
            View.PeriodChanged += (s, e) => Controller.GetInstance<IFragmentsProvider>().SetDefaultPeriod(View.SelectedPeriod);
        }

        public void ShowPeriods(bool visible)
        {
            if (visible)
            {
                View.SelectedPeriod = Controller.GetInstance<IFragmentsProvider>().GetDefaultPeriod();
                View.ShowIt();
            }
            else
            {
                View.HideIt();
            }
        }
    }
}
