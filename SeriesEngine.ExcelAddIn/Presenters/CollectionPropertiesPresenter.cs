using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.ExcelAddIn.Models;
using System.Xml.Linq;
using SeriesEngine.ExcelAddIn.Properties;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class CollectionPropertiesPresenter : Presenter<ICollectionPropertiesView>,
        ICommand<InsertCollectionBlockCommandArgs>,
        //ICommand<EditCollectionBlockCommandArgs>,
        ICommand<InsertSampleCollectionBlockCommandArgs>
    {
        private readonly IDataBlockProvider _dataBlockProvider;

        public CollectionPropertiesPresenter(ICollectionPropertiesView view, IApplicationController controller, IDataBlockProvider dataBlockProvider) :
            base(view, controller)
        {
            _dataBlockProvider = dataBlockProvider;

            View.CollectionChanged += (s, e) =>
            {
                dataBlockProvider.AddDataBlock(View.CollectionDataBlock);
                Controller.Execute(new ShowCustomPaneCommandArgs
                {
                    IsVisible = true,
                    ViewNameToOpen = ViewNames.DataBlocksViewName
                });
                Controller.Execute(new SelectDataBlockCommandArgs
                {
                    SelectedDataBlock = View.CollectionDataBlock
                });
            };
        }

        void ICommand<InsertSampleCollectionBlockCommandArgs>.Execute(InsertSampleCollectionBlockCommandArgs commandData)
        {
            var doc = XDocument.Parse(commandData.Query);
            var selection = commandData.CurrentSelection as ExcelCurrentSelection;
            var newBlock = (CollectionDataBlock)DataBlockConverter.GetDataBlock(doc, Period.Default);
            newBlock.Name = selection.Name;
            newBlock.Sheet = selection.Sheet;
            newBlock.StartCell = selection.Cell;

            _dataBlockProvider.AddDataBlock(newBlock);
            commandData.InsertedBlockName = newBlock.Name;
        }

        void ICommand<InsertCollectionBlockCommandArgs>.Execute(InsertCollectionBlockCommandArgs commandData)
        {
            var selection = commandData.CurrentSelection as ExcelCurrentSelection;
            var newBlock = new CollectionDataBlock
            {
                Name = selection.Name,
                Sheet = selection.Sheet,
                StartCell = selection.Cell
            };

            View.CollectionDataBlock = newBlock;
            View.ShowIt();
        }

        //void ICommand<EditCollectionBlockCommandArgs>.Execute(EditCollectionBlockCommandArgs commandData)
        //{
        //    View.CollectionDataBlock = (CollectionDataBlock)commandData.CollectionDataBlockToEdit;
        //    View.ShowIt();
        //}
    }
}
