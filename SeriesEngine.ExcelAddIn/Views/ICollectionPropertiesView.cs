using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface ICollectionPropertiesView : IView
    {
        event EventHandler CollectionChanged;
        CollectionDataBlock CollectionDataBlock { get; set; }
        void ShowIt();
    }
}
