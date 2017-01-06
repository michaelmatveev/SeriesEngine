using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.ExcelAddIn.Views;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class DataBlockPropertiesPresenter : Presenter<IDataBlockPropertiesView>//,
//        ICommand<InsertDataBlockCommandArgs>
    {
        public DataBlockPropertiesPresenter(IDataBlockPropertiesView view, IApplicationController controller, IDataBlockProvider dataBlockProvider) 
            : base(view, controller)
        {
            View.VariableBlockChanged += (s, e) =>
            {
                //dataBlockProvider.AddDataBlock(View.DataBlock);
                Controller.Execute(new ShowCustomPaneCommandArgs
                {
                    IsVisible = true,
                    ViewNameToOpen = ViewNames.DataBlocksViewName
                });
                Controller.Execute(new SelectDataBlockCommandArgs
                {
                    SelectedDataBlock = View.DataBlock
                });
            };
        }

        //public void EditFragment(CollectionDataBlock fragment)
        //{
        //    View.DataBlock = fragment;
        //    View.ShowIt();
        //}

        //void ICommand<InsertDataBlockCommandArgs>.Execute(InsertDataBlockCommandArgs commandData)
        //{
        //    var newBlock = new CollectionDataBlock
        //    {
        //        Name = commandData.Name,
        //        Sheet = commandData.Sheet,
        //        Cell = commandData.Cell
        //    };

        //    EditFragment(newBlock);
        //}
    }
}
