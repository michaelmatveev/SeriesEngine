using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class FilterPresenter : Presenter<IFilterView>
    {
        public FilterPresenter(IFilterView view, IController controller) : base(view, controller)
        {
        }

        public void ShowFilterForNetwork(Network network)
        {
            if(network == null)
            {
                View.HideIt();                
            }
            else
            {
                View.ShowIt(PaneLocation.Top);
            }
        }
    }
}
