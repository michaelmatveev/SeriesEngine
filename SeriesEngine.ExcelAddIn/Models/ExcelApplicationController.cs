using Microsoft.Office.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.ExcelAddIn.Presenters;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ExcelApplicationController : ApplicationController
    {        
        public Ribbon MainRibbon { get; set; }
        public CustomTaskPaneCollection PaneCollection { get; set; }
        public Workbook CurrentDocument { get; set; }      

        public void Configure()
        {
            Container.Configure(_ =>
            {
                _.For<IViewEmbedder>()
                    .Singleton()
                    .Use<PanesManager>()
                    .Ctor<CustomTaskPaneCollection>()
                    .Is(PaneCollection);              
 
                _.For<IController>()
                    .Use(this);

                _.For<IMainMenuView>()
                    .Use(MainRibbon);

                _.ForConcreteType<MainMenuPresenter>()
                    .Configure
                    .Singleton();

                _.For<IFragmentView>()
                    .Singleton()
                    .Use<Fragments>();

                _.ForConcreteType<FragmentPresenter>()
                    .Configure
                    .Singleton();

                _.For<IPeriodView>()
                    .Singleton()
                    .Use<PeriodSelector>();

                _.ForConcreteType<PeriodSelectorPresenter>();

                _.For<IFilterView>()
                    .Singleton()
                    .Use<Filter>();

                _.ForConcreteType<FilterPresenter>();

                _.For<IFragmentsProvider>()
                    .Singleton()
                    .Use<MockFragmentsProvider>();

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
                //.Configure
                //.Singleton();

                _.For<IModelProvider>()
                    .Singleton()
                    .Use<MockModelProvider>();

                _.For<IFragmentPropertiesView>()
                    //.Singleton()
                    .Use<FragmentProperties>()
                    .Ctor<IList<string>>()
                    .Is(CurrentDocument.Worksheets.OfType<Microsoft.Office.Interop.Excel.Worksheet>().Select(ws => ws.Name).ToList());

            });
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
