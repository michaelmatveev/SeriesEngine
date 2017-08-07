using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.App.EventData;
using SeriesEngine.ExcelAddIn.Business;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class MainMenuPresenter : Presenter<IMainMenuView>, 
        ICommand<InitalizeCommandArgs>, 
        IEventHandler<InitializeEventData>, 
        IEventHandler<MainPaneClosed>,
        IEventHandler<RestoreMenuStateEventData>
    {
        private readonly INetworksProvider _networkProvider;
        private readonly IStoredQueriesProvider _storedQueriesProvider;
        private bool _firstInitialization = true;
        private bool _isPaneVisible;

        public MainMenuPresenter(IMainMenuView view, 
            IApplicationController controller,
            ISelectionProvider selectionProvider,
            INetworksProvider networkProvider,
            IStoredQueriesProvider storedQueriesProvider) : base(view, controller)
        {
            _networkProvider = networkProvider;
            _storedQueriesProvider = storedQueriesProvider;

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
            View.MergeAll += (s, e) => Controller.Execute(new MergeAllCommandArgs());

            View.InsertNewDataBlock += (s, e) => Controller.Execute(new InsertCollectionBlockCommandArgs
            {
                CurrentSelection = selectionProvider.GetSelection()
            });

            View.InsertSampleBlock += (s, e) => 
            {
                var cmd = new InsertSampleCollectionBlockCommandArgs
                {
                    Query = e.XmlQuery,
                    CurrentSelection = selectionProvider.GetSelection()
                };
                Controller.Execute(cmd);
                Controller.Execute(new ReloadDataBlockCommandArgs
                {
                    BlockName = cmd.InsertedBlockName
                });
            };

            View.RenameObject += (s, e) => Controller.Execute(new RenameObjectCommandArgs
            {
                CurrentSelection = selectionProvider.GetSelection()
            });

            View.DeleteObject += (s, e) => Controller.Execute(new DeleteObjectCommandArgs
            {
                CurrentSelection = selectionProvider.GetSelection()
            });

            View.EditVariable += (s, e) => Controller.Execute(new EditPeriodVariableCommandArg
            {
                CurrentSelection = selectionProvider.GetSelection()
            });

            View.ReloadStoredQueries += (s, e) =>
            {
                if (controller.CurrentSolution != null)
                {
                    var modelName = controller.CurrentSolution.ModelName;
                    View.UpdateListOfStoredQueries(_storedQueriesProvider.GetStoredQueries(modelName));
                }
                else
                {
                    View.UpdateListOfStoredQueries(null);
                }
            };

            View.EditStoredQueries += (s, e) =>
            {
                Controller.Execute(new ShowStoredQueriesCommandArgs());
            };

            View.Connect += (s, e) => Controller.Execute(new SelectSolutionCommandArgs());
            View.Disconnect += (s, e) => Controller.CurrentSolution = null;
        }


        void ICommand<InitalizeCommandArgs>.Execute(InitalizeCommandArgs commandData)
        {
        }

        void IEventHandler<InitializeEventData>.Handle(InitializeEventData eventData)
        {
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
