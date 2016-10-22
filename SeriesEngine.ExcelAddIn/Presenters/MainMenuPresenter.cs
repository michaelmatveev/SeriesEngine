using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Models.Fragments;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class MainMenuPresenter : Presenter<IMainMenuView>
    {
        public MainMenuPresenter(IMainMenuView view, IController controller) : base(view, controller)
        {
            View.ShowFragmentsPane += (s, e) =>
            {
                if (Controller.IsActive)
                {
                    Controller.GetInstance<FragmentPresenter>().ShowFragments(e.Visible);
                }
            };

            View.ShowPeriodSelectorPane += (s, e) =>
            {
                if (Controller.IsActive)
                {
                    Controller.GetInstance<PeriodSelectorPresenter>().ShowPeriods(e.Visible);
                }
            };

            View.FilterSelected += (s, e) =>
            {
                if (Controller.IsActive)
                {
                    Controller.GetInstance<FilterPresenter>().ShowFilterForNetwork(e.SelectedNetwork);
                }
            };

            View.RefreshAll += (s, e) =>
            {
                if (Controller.IsActive)
                {
                    var framgmentsProvider = Controller.GetInstance<IFragmentsProvider>();
                    var dataImporter = Controller.GetInstance<IDataImporter>();
                    dataImporter.ImportFromFragments(
                        framgmentsProvider.GetFragments(string.Empty).OfType<SheetFragment>(),
                        framgmentsProvider.GetDefaultPeriod());
                }
            };

            View.SaveAll += (s, e) =>
            {
                if (Controller.IsActive)
                {
                    var framgmentsProvider = Controller.GetInstance<IFragmentsProvider>();
                    var dataExporter = Controller.GetInstance<IDataExporter>();
                    dataExporter.ExportFromFragments(framgmentsProvider.GetFragments(string.Empty).OfType<SheetFragment>());
                }
            };
        }

        public void Run()
        {
            View.InitializeFilters(Controller.GetInstance<INetworksProvider>().GetNetworks(Controller.Filter));
        }

        public void SetFragmentsButton(bool state)
        {
            View.SetFragmentsButtonState(state);   
        }

        public void SetPeriodButton(bool state)
        {
            View.SetPeriodButtonState(state);
        }

    }
}
