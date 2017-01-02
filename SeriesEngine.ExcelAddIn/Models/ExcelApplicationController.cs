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

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ExcelApplicationController : ApplicationController
    {        
        public Ribbon MainRibbon { get; set; }
        public CustomTaskPaneCollection PaneCollection { get; set; }
        public Workbook CurrentDocument { get; set; }
        //public bool IsActive { get; set; }

        public void Configure()
        {
            Container.Configure(_ =>
            {
                _.For<IApplicationController>()
                    .Use(this);

                _.ForConcreteType<PanesManager>()
                    .Configure
                    .Singleton()
                    .Ctor<CustomTaskPaneCollection>()
                    .Is(PaneCollection);                 

                _.For<IMainMenuView>()
                    .Use(MainRibbon);

                _.ForConcreteType<MainMenuPresenter>()
                    .Configure
                    .Singleton()
                    .InterceptWith(new FuncInterceptor<MainMenuPresenter>(m => RegisterHandlers(m)));

                _.For<ICommand<InitalizeCommandArgs>>()
                    .Use(c => c.GetInstance<MainMenuPresenter>());

                _.For<IFragmentView>()
                    .Singleton()
                    .Use<FragmentsControl>();

                _.ForConcreteType<FragmentPresenter>()
                    .Configure
                    .Singleton();

                _.For<IPeriodView>()
                    .Singleton()
                    .Use<PeriodSelector>();

                _.ForConcreteType<PeriodSelectorPresenter>()
                    .Configure
                    .Singleton();

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
                    .Use<WorkbookFragmentsProvider>();

                _.For<INetworksProvider>()
                    .Singleton()
                    //.Use<MockNetworkProvider>();
                    .Use<DataBaseNetworkProvider>();

                _.For<Workbook>()
                    .Use(CurrentDocument);

                _.ForConcreteType<DataImporter>()
                    .Configure
                    .Singleton();

                _.For<ICommand<ReloadAllCommandArgs>>()
                    .Use(c => c.GetInstance<DataImporter>());

                _.ForConcreteType<DataExporter>()
                    .Configure
                    .Singleton();

                _.For<ICommand<SaveAllCommandArgs>>()
                    .Use(c => c.GetInstance<DataExporter>());

                _.ForConcreteType<FragmentPropertiesPresenter>();

                _.For<IModelProvider>()
                    .Singleton()
                    .Use<MockModelProvider>();

                _.For<IFragmentPropertiesView>()
                    .Use<FragmentProperties>()
                    .Ctor<IList<string>>()
                    .Is(CurrentDocument.Worksheets.OfType<Microsoft.Office.Interop.Excel.Worksheet>().Select(ws => ws.Name).ToList());

            });

            Container.GetInstance<MainMenuPresenter>();            
        }

        private T RegisterHandlers<T>(T eventHandler)
        {
            EventPublisher.RegisterHandlers(eventHandler);
            return eventHandler;
        }

    }
}
