using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.App.EventData;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class FragmentPresenter : Presenter<IFragmentView>, ICommand<SwitchToFragmentsCommandArgs>
    {
        private IFragmentsProvider _fragmentsProvider;
        public FragmentPresenter(IFragmentView view, IApplicationController controller, IFragmentsProvider fragmentsProvider) : base(view, controller)
        {
            _fragmentsProvider = fragmentsProvider;
            //View.PaneClosed += (s, e) => Controller.GetInstance<MainMenuPresenter>().SetFragmentsButton(false);
            //View.FragmentSelected += (s, e) =>
            //{
            //    Controller.GetInstance<FragmentPropertiesPresenter>().EditFragment(e.Fragment);
            //    ShowFragments(true); // refresh view after edit
            //};
            //View.NewFragmentRequested += (s, e) =>
            //{
            //    var newFragment = Controller.GetInstance<IFragmentsProvider>().CreateFragment(e.SourceCollection);
            //    Controller.GetInstance<FragmentPropertiesPresenter>().EditFragment(newFragment);
            //    ShowFragments(true);
            //};
            //View.FragmentDeleted += (s, e) =>
            //{
            //    Controller.GetInstance<IFragmentsProvider>().DeleteFragment(e.Fragment);
            //    ShowFragments(true);
            //};
            //View.FragmentCopied += (s, e) =>
            //{
            //    var copiedFragment = Controller.GetInstance<IFragmentsProvider>().CopyFragment(e.Fragment, e.SourceCollection);
            //    Controller.GetInstance<IFragmentsProvider>().AddFragment(copiedFragment);
            //    ShowFragments(true);
            //};
        }

        public void ShowFragments(bool visible)
        {
            //if (visible)
            //{
            //    View.RefreshFragmentsView(Controller.GetInstance<IFragmentsProvider>().GetFragments(Controller.Filter));
            //    View.ShowIt();
            //}
            //else
            //{
            //    View.HideIt();
            //}
        }

        void ICommand<SwitchToFragmentsCommandArgs>.Execute(SwitchToFragmentsCommandArgs commandData)
        {
            View.RefreshFragmentsView(_fragmentsProvider.GetFragments(string.Empty));
            Controller.Raise(new SwitchToViewEventData
            {
                InflatedControl = (Control)View
            });
        }

    }
}
