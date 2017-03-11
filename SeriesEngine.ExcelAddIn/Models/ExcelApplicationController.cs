using Microsoft.Office.Tools;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Tools.Excel;
using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.ExcelAddIn.Presenters;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using StructureMap.Building.Interception;
using SeriesEngine.App.EventData;
using System;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ExcelApplicationController : ApplicationController
    {
        //private readonly Lazy<Workbook> _currentDocument;
        private readonly Workbook _currentDocument;
        private readonly RibbonWrapper _mainRibbon;
        private readonly CustomTaskPaneCollection _paneCollection;

        public ExcelApplicationController()
        {

        }

        public ExcelApplicationController(Workbook workbook, RibbonWrapper mainRibbon, CustomTaskPaneCollection paneCollection)
        {
            _currentDocument = workbook;
            _mainRibbon = mainRibbon;
            _paneCollection = paneCollection;           
        }

        public void Configure()
        {
            Container.Configure(_ =>
            {
                _.For<IApplicationController>()
                    .Use(this);

                _.For<ISelectionProvider>()
                    .Singleton()
                    .Use<SelectionProvider>();

                _.ForConcreteType<PanesManager>()
                    .Configure
                    .Singleton()
                    .Ctor<CustomTaskPaneCollection>()
                    .Is(_paneCollection);

                _.For<IMainMenuView>()
                    .Use(_mainRibbon);

                _.ForConcreteType<MainMenuPresenter>()
                    .Configure
                    .Singleton()
                    .InterceptWith(new FuncInterceptor<MainMenuPresenter>(m => RegisterHandlers(m)));

                _.For<ICommand<InitalizeCommandArgs>>()
                    .Use(c => c.GetInstance<MainMenuPresenter>());

                _.For<IDataBlockView>()
                    .Singleton()
                    .Use<DataBlocksControl>();

                _.ForConcreteType<DataBlockPresenter>()
                    .Configure
                    .Singleton();

                _.For<ICommand<SwitchToDataBlocksCommandArgs>>()
                    .Use(c => c.GetInstance<DataBlockPresenter>());
                _.For<ICommand<SelectDataBlockCommandArgs>>()
                    .Use(c => c.GetInstance<DataBlockPresenter>());

                _.For<IPeriodView>()
                    .Singleton()
                    .Use<PeriodSelector>();

                _.ForConcreteType<PeriodSelectorPresenter>()
                    .Configure
                    .Singleton();

                _.For<ICommand<SwitchToPeriodCommandArgs>>()
                    .Use(c => c.GetInstance<PeriodSelectorPresenter>());

                _.For<IMainPane>()
                    .Singleton()
                    .Use<MainPaneControl>();

                _.ForConcreteType<MainPanePresenter>()
                    .Configure
                    .Singleton()
                    .InterceptWith(new FuncInterceptor<MainPanePresenter>(m => RegisterHandlers(m)));

                _.For<ICommand<ShowCustomPaneCommandArgs>>()
                    .Use(c => c.GetInstance<MainPanePresenter>());

                _.For<IFilterView>()
                    .Singleton()
                    .Use<Filter>();

                _.ForConcreteType<FilterPresenter>();

                _.For<IDataBlockProvider>()
                    .Singleton()
                    .Use<WorkbookDataBlockProvider>();

                _.For<INetworksProvider>()
                    .Singleton()
                    //.Use<MockNetworkProvider>();
                    .Use<DataBaseNetworkProvider>();

                _.For<Workbook>()
                    .Use(_currentDocument);

                _.ForConcreteType<DataImporter>()
                    .Configure
                    //.Ctor<int>("solutionId")
                    //.Is(CurrentSolutionId)
                    .Singleton();

                _.For<ICommand<ReloadAllCommandArgs>>()
                    .Use(c => c.GetInstance<DataImporter>());

                _.ForConcreteType<DataExporter>()
                    .Configure
                    .Singleton()
                    .OnCreation(de => RegisterErrorHandler(de));
                    //.InterceptWith(new FuncInterceptor<DataExporter>(m => RegisterErrorHandler(m)));

                _.For<ICommand<SaveAllCommandArgs>>()
                    .Use(c => c.GetInstance<DataExporter>());

                _.ForConcreteType<CollectionPropertiesPresenter>();
                
                _.For<ICollectionPropertiesView>()
                    .Use<CollectionProperties>()
                    .Ctor<Func<IList<string>>>()
                    .Is(GetWorksheetsName);

                _.ForConcreteType<NodeBlockPropertiesPresenter>();

                _.For<IModelProvider>()
                    .Singleton()
                    .Use<MockModelProvider>();

                _.For<IDataBlockPropertiesView>()
                    .Use<NodeBlockProperties>()
                    .Ctor<IList<string>>()
                    .Is(GetWorksheetsName());

                _.For<ISolutionProvider>()
                    .Singleton()
                    .Use<SolutionProvider>();

                _.For<ISolutionSelectorView>()
                    .Use<SolutionSelector>();

                _.ForConcreteType<SolutionSelectorPresenter>();

                _.For<IObjectProvider>()
                    .Singleton()
                    .Use<ObjectProvider>();

                _.For<IObjectPropertiesView>()
                    .Use<ObjectProperties>();

                _.ForConcreteType<ObjectPropertiesPresenter>();
            });

            var provider = Container.GetInstance<IDataBlockProvider>();
            var solutionProver = Container.GetInstance<ISolutionProvider>();
            var id = provider.GetLastSolutionId();
            if (id != 0)
            {
                CurrentSolution = solutionProver.GetSolutionById(id);
            }
            Container.GetInstance<MainMenuPresenter>();
            //Raise(new InitializeEventData());
        }

        private IList<string> GetWorksheetsName()
        {
            var result = _currentDocument
                .Worksheets
                .OfType<Microsoft.Office.Interop.Excel.Worksheet>()
                .Select(ws => ws.Name)
                .ToList();

            return result;
        }

        private T RegisterHandlers<T>(T eventHandler)
        {
            EventPublisher.RegisterHandlers(eventHandler);
            return eventHandler;
        }

        private void RegisterErrorHandler(IErrorAware errorProvider)
        {
            errorProvider.ErrorOccured += (e, args) =>
            {
                var message = $"{args.Message}{Environment.NewLine}Завершить текущую операцию?";
                var result = MessageBox.Show(message, ViewNames.ApplicationCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                args.Cancel = result == DialogResult.Yes;
            };
        }

        public void Activate()
        {
            _mainRibbon.SetTabVisibleState(true);
            _mainRibbon.IsActive = true;
            Raise(new RestoreMenuStateEventData());
        }

        public void Deactivate()
        {
            _mainRibbon.SetTabVisibleState(false);
            _mainRibbon.IsActive = false;
            //Execute(new ShowCustomPaneCommandArgs
            //{
            //    IsVisible = false
            //});
        }

        public void StopGettingEventsFromRibbon()
        {
            _mainRibbon.IsActive = false;
        }

        public void PreserveDataBlocks()
        {
            Execute(new PreserveDataBlocksCommandArgs());
        }

    }
}