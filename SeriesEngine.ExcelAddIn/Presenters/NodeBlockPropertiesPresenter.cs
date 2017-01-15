using System;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.ExcelAddIn.Views;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class NodeBlockPropertiesPresenter : Presenter<IDataBlockPropertiesView>,
        ICommand<InsertNodeDataBlockCommandArgs>,
        ICommand<EditNodeDataBlockCommandArgs>
    {
        public NodeBlockPropertiesPresenter(IDataBlockPropertiesView view, IApplicationController controller, IDataBlockProvider dataBlockProvider) 
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

        void ICommand<InsertNodeDataBlockCommandArgs>.Execute(InsertNodeDataBlockCommandArgs commandData)
        {
            var newBlock = new NodeDataBlock((CollectionDataBlock)commandData.Parent);
            View.DataBlock = newBlock;
            View.ShowIt();
        }

        void ICommand<EditNodeDataBlockCommandArgs>.Execute(EditNodeDataBlockCommandArgs commandData)
        {
            View.DataBlock = (NodeDataBlock)commandData.NodeDataBlockToEdit;
            View.ShowIt();
        }
    }
}