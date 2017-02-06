using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.App.EventData;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class MainMenuPresenter : Presenter<IMainMenuView>, 
        ICommand<InitalizeCommandArgs>, 
        IEventHandler<InitializeEventData>, 
        IEventHandler<MainPaneClosed>,
        IEventHandler<RestoreMenuStateEventData>
    {
        private INetworksProvider _networkProvider;
        private bool _firstInitialization = true;
        private bool _isPaneVisible;

        public MainMenuPresenter(IMainMenuView view, 
            IApplicationController controller,
            INetworksProvider networkProvider) : base(view, controller)
        {
            _networkProvider = networkProvider;
            View.ShowCustomPane += (s, e) =>
            {
                _isPaneVisible = e.Visible;
                Controller.Execute(new ShowCustomPaneCommandArgs
                {
                    IsVisible = _isPaneVisible
                });

                if (_firstInitialization)
                {
                    Controller.Execute(new SwitchToPeriodCommandArgs());
                    _firstInitialization = false;
                }
            };

            View.RefreshAll += (s, e) => Controller.Execute(new ReloadAllCommandArgs());
            View.SaveAll += (s, e) => Controller.Execute(new SaveAllCommandArgs());

            View.InsertNewDataBlock += (s, e) => Controller.Execute(new InsertCollectionBlockCommandArgs
            {
                Name = e.Name,
                Cell = e.Cell,
                Sheet = e.Sheet
            });

            View.InsertSampleBlock += (s, e) => Controller.Execute(new InsertSampleCollectionBlockCommandArgs
            {
                Name = e.Name,
                Cell = e.Cell,
                Sheet = e.Sheet
            });
        }


        void ICommand<InitalizeCommandArgs>.Execute(InitalizeCommandArgs commandData)
        {
           // View.InitializeFilters(_networkProvider.GetNetworks(string.Empty));
        }

        void IEventHandler<InitializeEventData>.Handle(InitializeEventData eventData)
        {
            //Controller.CurrentSolutionId = 59;
           // View.InitializeFilters(_networkProvider.GetNetworks(string.Empty));
        }

        void IEventHandler<MainPaneClosed>.Handle(MainPaneClosed eventData)
        {
            View.SetButtonToggleState(_isPaneVisible = false);
        }
                
        void IEventHandler<RestoreMenuStateEventData>.Handle(RestoreMenuStateEventData eventData)
        {
            View.SetButtonToggleState(_isPaneVisible);
        }

    }
}
