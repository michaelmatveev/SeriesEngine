using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.App.EventData;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class PeriodSelectorPresenter : Presenter<IPeriodView>, ICommand<ShowPeriodCommandArgs>
    {
        private IFragmentsProvider _fragmentsProvider;
        public PeriodSelectorPresenter(IPeriodView view, IApplicationController controller, IFragmentsProvider fragmentsProvider) : base(view, controller)
        {
            _fragmentsProvider = fragmentsProvider;
            //View.PaneClosed += (s, e) => Controller.GetInstance<MainMenuPresenter>().SetPeriodButton(false);
            View.PaneClosed += (s, e) => Controller.Raise(new TogglePeriodEventData());
            View.PeriodChanged += (s, e) => _fragmentsProvider.SetDefaultPeriod(View.SelectedPeriod);
        }

        private void ShowPeriods(bool visible)
        {
            if (visible)
            {
                View.SelectedPeriod = _fragmentsProvider.GetDefaultPeriod();
                View.ShowIt();
            }
            else
            {
                View.HideIt();
            }
        }

        void ICommand<ShowPeriodCommandArgs>.Execute(ShowPeriodCommandArgs commandData)
        {
            ShowPeriods(commandData.IsVisible);
        }
    }
}
