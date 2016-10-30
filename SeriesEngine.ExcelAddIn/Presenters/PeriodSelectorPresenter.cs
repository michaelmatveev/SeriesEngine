using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.App.EventData;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class PeriodSelectorPresenter : Presenter<IPeriodView>, ICommand<SwitchToPeriodCommandArgs>
    {
        private IFragmentsProvider _fragmentsProvider;
        public PeriodSelectorPresenter(IPeriodView view, IApplicationController controller, IFragmentsProvider fragmentsProvider) : base(view, controller)
        {
            _fragmentsProvider = fragmentsProvider;
            View.PeriodChanged += (s, e) => _fragmentsProvider.SetDefaultPeriod(View.SelectedPeriod);
        }

        public void Execute(SwitchToPeriodCommandArgs commandData)
        {
            Controller.Raise(new SwitchToViewEventData
            {
                InflatedControl = (Control)View
            });
        }
    }
}
