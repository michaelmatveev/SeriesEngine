using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.Msk1;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class FilterPresenter : Presenter<IFilterView>
    {
        public FilterPresenter(IFilterView view, IApplicationController controller) : base(view, controller)
        {
            //View.FilterUpdated += (s, e) =>
            //{
            //    if (Controller.IsActive)
            //    {
            //        Controller.Filter = e.FilterString;
            //    }
            //};
        }

        public void ShowFilterForNetwork(Network network)
        {
            if(network == null)
            {
                View.HideIt();                
            }
            else
            {
                View.RefreshFilter(network as NetworkTree, string.Empty);
                View.ShowIt(PaneLocation.Top);
            }
        }
    }
}
