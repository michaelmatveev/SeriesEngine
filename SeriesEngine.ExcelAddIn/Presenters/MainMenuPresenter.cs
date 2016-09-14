using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class MainMenuPresenter : Presenter<IMainMenuView>
    {
        public MainMenuPresenter(IMainMenuView view, IController controller) : base(view, controller)
        {
            View.ShowFragmentsPane += (s, e) => Controller.GetInstance<FragmentPresenter>().ShowFragments();
            View.ShowPeriodSelectorPane += (s, e) => Controller.GetInstance<PeriodSelectorPresenter>().ShowPeriods();
        }

        public void Run()
        {
        }
    }
}
