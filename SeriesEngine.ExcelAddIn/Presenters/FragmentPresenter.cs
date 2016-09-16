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
            View.PaneClosed += (s, e) => Controller.GetInstance<MainMenuPresenter>().SetFragmentsButton(false);
        }

        public void ShowFragments(bool visible)
        {
            if (visible)
            {
                View.RefreshFragmentsView(Controller.GetInstance<IFragmentsProvider>().GetFragments());
                View.ShowIt();
            }
            else
            {
                View.HideIt();
            }
        }
    }
}
