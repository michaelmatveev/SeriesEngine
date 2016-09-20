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

                _.ForConcreteType<FragmentPropertiesPresenter>();
                    //.Configure
                    //.Singleton();

                _.For<IFragmentPropertiesView>()                    
                    //.Singleton()
                    .Use<FragmentProperties>();

            });
        }
    }
}
