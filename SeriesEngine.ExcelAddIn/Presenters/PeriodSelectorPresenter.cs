using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.App.EventData;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class PeriodSelectorPresenter : Presenter<IPeriodView>, 
        ICommand<SwitchToPeriodCommandArgs>
    {
        private IDataBlockProvider _dataBlockProvider;
        public PeriodSelectorPresenter(IPeriodView view, IApplicationController controller, IDataBlockProvider fragmentsProvider) : base(view, controller)
        {
            _dataBlockProvider = fragmentsProvider;
            View.PeriodChanged += (s, e) =>
            {
                _dataBlockProvider.SetDefaultPeriod(View.SelectedPeriod);
                Controller.Execute(new ReloadAllCommandArgs());
            };
        }

        public void Execute(SwitchToPeriodCommandArgs commandData)
        {
            View.SelectedPeriod = _dataBlockProvider.GetDefaultPeriod();
            Controller.Raise(new SwitchToViewEventData
            {
                InflatedControl = (Control)View
            });
        }
    }
}
