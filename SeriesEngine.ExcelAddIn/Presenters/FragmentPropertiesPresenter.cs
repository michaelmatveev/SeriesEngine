using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.App;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class FragmentPropertiesPresenter : Presenter<IFragmentPropertiesView>
    {
        public FragmentPropertiesPresenter(IFragmentPropertiesView view, IApplicationController controller) : base(view, controller)
        {
            //View.FragmentChanged += (s, e) =>
            //{
            //    Controller.GetInstance<IFragmentsProvider>().AddFragment(View.Fragment);
            //};
        }

        public void EditFragment(DataBlock fragment)
        {
            View.Fragment = fragment;
            View.ShowIt();
        }

    }
}
