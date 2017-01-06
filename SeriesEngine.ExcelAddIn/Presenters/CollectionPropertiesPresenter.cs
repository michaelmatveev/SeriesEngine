﻿using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class CollectionPropertiesPresenter : Presenter<ICollectionPropertiesView>,
        ICommand<InsertCollectionBlockCommandArgs>,
        ICommand<EditCollectionBlockCommandArgs>        
    {
        public CollectionPropertiesPresenter(ICollectionPropertiesView view, IApplicationController controller, IDataBlockProvider dataBlockProvider) :
            base(view, controller)
        {
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

        void ICommand<InsertCollectionBlockCommandArgs>.Execute(InsertCollectionBlockCommandArgs commandData)
        {
            var newBlock = new CollectionDataBlock
            {
                Name = commandData.Name,
                Sheet = commandData.Sheet,
                Cell = commandData.Cell
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
