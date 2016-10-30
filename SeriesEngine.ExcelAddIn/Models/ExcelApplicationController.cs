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
using System.Linq.Expressions;
using System;
using SeriesEngine.App.EventData;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ExcelApplicationController : ApplicationController
    {        
        public Ribbon MainRibbon { get; set; }
        public CustomTaskPaneCollection PaneCollection { get; set; }
        public Workbook CurrentDocument { get; set; }
        public bool IsActive { get; set; }

        public void Configure()
        {
            Container.Configure(_ =>
            {
                _.For<IApplicationController>()
                    .Use(this);

                //_.For<IEventPublisher>()
                //    .Singleton()
                //    .Use<EventPublisher>();

                _.For<IViewEmbedder>()
                    .Singleton()
                    .Use<PanesManager>()
                    .Ctor<CustomTaskPaneCollection>()
                    .Is(PaneCollection);

                _.For<IMainMenuView>()
                    .Use(MainRibbon);

                _.ForConcreteType<MainMenuPresenter>()
                    .Configure
                    .Singleton()
                    .InterceptWith(new FuncInterceptor<MainMenuPresenter>(m => RegisterHandlers(m)));

                _.For<ICommand<InitalizeCommandArgs>>().Use(c => c.GetInstance<MainMenuPresenter>());

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

                _.For<ICommand<ShowPeriodCommandArgs>>().Use(c => c.GetInstance<PeriodSelectorPresenter>());

                _.For<IFilterView>()
                    .Singleton()
                    .Use<Filter>();

                _.ForConcreteType<FilterPresenter>();

                _.For<IFragmentsProvider>()
                    .Singleton()
                    .Use<WorkbookFragmentsProvider>();

                _.For<INetworksProvider>()
                    .Singleton()
                    .Use<MockNetworkProvider>();

                _.For<Workbook>()
                    .Use(CurrentDocument);

                _.For<IDataImporter>()
                    .Singleton()
                    .Use<DataImporter>();

                _.For<IDataExporter>()
                    .Singleton()
                    .Use<DataExporter>();

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

        private bool isPeriodPaneOpen;
        private bool isFragmentPaneOpen;

        public void SaveRibbonState()
        {
            isPeriodPaneOpen = MainRibbon.toggleButtonShowPeriodSelector.Checked;
            isFragmentPaneOpen = MainRibbon.toggleButtonShowFragmetns.Checked;
        }

        public void RestoreRibbonState()
        {
            MainRibbon.toggleButtonShowPeriodSelector.Checked = isPeriodPaneOpen;
            MainRibbon.toggleButtonShowFragmetns.Checked = isFragmentPaneOpen;
        }
    }
}
