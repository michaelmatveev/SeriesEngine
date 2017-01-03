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
using SeriesEngine.ExcelAddIn.Models.DataBlocks;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class DataBlockPresenter : Presenter<IDataBlockView>, 
        ICommand<SwitchToDataBlocksCommandArgs>,
        ICommand<SelectDataBlockCommandArgs>
    {
        private IDataBlockProvider _dataBlockProvider;
        public DataBlockPresenter(IDataBlockView view, IApplicationController controller, IDataBlockProvider dataBlockProvider) : base(view, controller)
        {
            _dataBlockProvider = dataBlockProvider;
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

        void ICommand<SelectDataBlockCommandArgs>.Execute(SelectDataBlockCommandArgs commandData)
        {
            View.RefreshDataBlockView(_dataBlockProvider.GetDataBlocks());
            View.SelectedBlock = (BaseDataBlock)commandData.SelectedDataBlock;
            //Controller.Raise(new SwitchToViewEventData
            //{
            //    InflatedControl = (Control)View
            //});
            View.ShowIt();
        }

        void ICommand<SwitchToDataBlocksCommandArgs>.Execute(SwitchToDataBlocksCommandArgs commandData)
        {
            View.RefreshDataBlockView(_dataBlockProvider.GetDataBlocks());
            Controller.Raise(new SwitchToViewEventData
            {
                InflatedControl = (Control)View
            });
        }

        //void ICommand<InsertDataBlockCommandArgs>.Execute(InsertDataBlockCommandArgs commandData)
        //{
        //    _fragmentsProvider.AddDataBlock(new CollectionDataBlock
        //    {
        //        Name = commandData.Name,
        //        Sheet = commandData.Sheet,
        //        Cell = commandData.Cell
        //    });

        //    View.RefreshDataBlockView(_fragmentsProvider.GetDataBlocks(string.Empty));
        //    Controller.Raise(new SwitchToViewEventData
        //    {
        //        InflatedControl = (Control)View
        //    });
        //    View.ShowIt();
        //}

    }
}
