﻿using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.App.EventData;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class MainMenuPresenter : Presenter<IMainMenuView>, 
        ICommand<InitalizeCommandArgs>, 
        IEventHandler<InitializeEventData>, 
        IEventHandler<MainPaneClosed>
    {
        private INetworksProvider _networkProvider;
        private bool _firstInitialization = true;

        public MainMenuPresenter(IMainMenuView view, 
            IApplicationController controller,
            INetworksProvider networkProvider) : base(view, controller)
        {
            _networkProvider = networkProvider;
            //View.ShowFragmentsPane += (s, e) =>
            //{
            //    if (Controller.IsActive)
            //    {
            //        Controller.GetInstance<FragmentPresenter>().ShowFragments(e.Visible);
            //    }
            //};

            View.ShowCustomPane += (s, e) =>
            {
                Controller.Execute(new ShowCustomPaneCommandArgs
                {
                    IsVisible = e.Visible
                });

                if (_firstInitialization)
                {
                    Controller.Execute(new SwitchToPeriodCommandArgs());
                    _firstInitialization = false;
                }
            };

            //View.FilterSelected += (s, e) =>
            //{
            //    if (Controller.IsActive)
            //    {
            //        Controller.GetInstance<FilterPresenter>().ShowFilterForNetwork(e.SelectedNetwork);
            //    }
            //};

            View.RefreshAll += (s, e) =>
            {
                Controller.Execute(new ReloadAllCommandArgs());
            };

            View.SaveAll += (s, e) =>
            {
                Controller.Execute(new SaveAllCommandArgs());
            };
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

        void IEventHandler<MainPaneClosed>.Handle(MainPaneClosed eventData)
        {
            View.SetPeriodButtonState(false);
        }

    }
}
