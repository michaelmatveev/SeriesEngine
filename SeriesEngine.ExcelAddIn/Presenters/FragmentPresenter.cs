using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class FragmentPresenter : Presenter<IFragmentView>
    {
        public FragmentPresenter(IFragmentView view, IController controller) : base(view, controller)
        {
        }

        public void ShowFragments()
        {
            View.ShowIt();
        }
    }
}
