using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class FragmentPropertiesPresenter : Presenter<IFragmentPropertiesView>
    {
        public FragmentPropertiesPresenter(IFragmentPropertiesView view, IController controller) : base(view, controller)
        {
            View.FragmentChanged += (s, e) =>
              {
                //View.Fragment;
            };
        }

        public void EditFragment(Fragment fragment)
        {
            View.Fragment = fragment;
            View.Networks = Controller.GetInstance<INetworksProvider>().GetNetworks();
            View.ShowIt();
        }
    }
}
