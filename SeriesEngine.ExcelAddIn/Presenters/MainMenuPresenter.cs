using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Models.Fragments;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.App.EventData;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class MainMenuPresenter : Presenter<IMainMenuView>, ICommand<InitalizeCommandArgs>, IEventHandler<InitializeEventData>, IEventHandler<TogglePeriodEventData>
    {
        private INetworksProvider _networkProvider;
        public MainMenuPresenter(IMainMenuView view, IApplicationController controller, INetworksProvider networkProvider) : base(view, controller)
        {
            _networkProvider = networkProvider;
            //View.ShowFragmentsPane += (s, e) =>
            //{
            //    if (Controller.IsActive)
            //    {
            //        Controller.GetInstance<FragmentPresenter>().ShowFragments(e.Visible);
            //    }
            //};

            View.ShowPeriodSelectorPane += (s, e) =>
            {
                Controller.Execute<ShowPeriodCommandArgs>(new ShowPeriodCommandArgs
                {
                    IsVisible = e.Visible
                });
                //if (Controller.IsActive)
                //{
                //    Controller.GetInstance<PeriodSelectorPresenter>().ShowPeriods(e.Visible);
                //}
            };

            //View.FilterSelected += (s, e) =>
            //{
            //    if (Controller.IsActive)
            //    {
            //        Controller.GetInstance<FilterPresenter>().ShowFilterForNetwork(e.SelectedNetwork);
            //    }
            //};

            //View.RefreshAll += (s, e) =>
            //{
            //    if (Controller.IsActive)
            //    {
            //        var framgmentsProvider = Controller.GetInstance<IFragmentsProvider>();
            //        var dataImporter = Controller.GetInstance<IDataImporter>();
            //        dataImporter.ImportFromFragments(
            //            framgmentsProvider.GetFragments(string.Empty).OfType<SheetFragment>(),
            //            framgmentsProvider.GetDefaultPeriod());
            //    }
            //};

            //View.SaveAll += (s, e) =>
            //{
            //    if (Controller.IsActive)
            //    {
            //        var framgmentsProvider = Controller.GetInstance<IFragmentsProvider>();
            //        var dataExporter = Controller.GetInstance<IDataExporter>();
            //        dataExporter.ExportFromFragments(framgmentsProvider.GetFragments(string.Empty).OfType<SheetFragment>());
            //    }
            //};
        }

        public void SetFragmentsButton(bool state)
        {
            View.SetFragmentsButtonState(state);   
        }

        void ICommand<InitalizeCommandArgs>.Execute(InitalizeCommandArgs commandData)
        {
            View.InitializeFilters(_networkProvider.GetNetworks(string.Empty));
        }

        void IEventHandler<InitializeEventData>.Handle(InitializeEventData eventData)
        {
            View.InitializeFilters(_networkProvider.GetNetworks(string.Empty));
        }

        void IEventHandler<TogglePeriodEventData>.Handle(TogglePeriodEventData eventData)
        {
            View.SetPeriodButtonState(false);
        }

    }
}
