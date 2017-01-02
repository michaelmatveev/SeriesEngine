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
        private IDataBlockProvider _fragmentsProvider;
        public PeriodSelectorPresenter(IPeriodView view, IApplicationController controller, IDataBlockProvider fragmentsProvider) : base(view, controller)
        {
            _fragmentsProvider = fragmentsProvider;
            View.PeriodChanged += (s, e) => _fragmentsProvider.SetDefaultPeriod(View.SelectedPeriod);
        }

        public void Execute(SwitchToPeriodCommandArgs commandData)
        {
            View.SelectedPeriod = _fragmentsProvider.GetDefaultPeriod();
            Controller.Raise(new SwitchToViewEventData
            {
                InflatedControl = (Control)View
            });
        }
    }
}
