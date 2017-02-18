using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.ExcelAddIn.Models;
using System.Xml.Linq;
using SeriesEngine.ExcelAddIn.Properties;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class CollectionPropertiesPresenter : Presenter<ICollectionPropertiesView>,
        ICommand<InsertCollectionBlockCommandArgs>,
        ICommand<EditCollectionBlockCommandArgs>,
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

        public void Execute(InsertSampleCollectionBlockCommandArgs commandData)
        {
            var doc = XDocument.Parse(Resources.TestGrid);
            var newBlock = (CollectionDataBlock)DataBlockConverter.GetDataBlock(doc, Period.Default);
            newBlock.Name = commandData.CurrentSelection.Name;
            newBlock.Sheet = commandData.CurrentSelection.Sheet;
            newBlock.Cell = commandData.CurrentSelection.Cell;

            _dataBlockProvider.AddDataBlock(newBlock);
        }

        void ICommand<InsertCollectionBlockCommandArgs>.Execute(InsertCollectionBlockCommandArgs commandData)
        {
            var newBlock = new CollectionDataBlock
            {
                Name = commandData.CurrentSelection.Name,
                Sheet = commandData.CurrentSelection.Sheet,
                Cell = commandData.CurrentSelection.Cell
            };

            View.CollectionDataBlock = newBlock;
            View.ShowIt();
        }

        void ICommand<EditCollectionBlockCommandArgs>.Execute(EditCollectionBlockCommandArgs commandData)
        {
            View.CollectionDataBlock = (CollectionDataBlock)commandData.CollectionDataBlockToEdit;
            View.ShowIt();
        }
    }
}
