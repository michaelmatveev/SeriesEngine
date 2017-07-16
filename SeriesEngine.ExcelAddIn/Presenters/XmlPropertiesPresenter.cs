using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.App;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class XmlPropertiesPresenter : Presenter<IXmlPropertiesView>,
        ICommand<EditCollectionBlockCommandArgs>
    {
        private readonly IDataBlockProvider _dataBlockProvider;
        public XmlPropertiesPresenter(IXmlPropertiesView view, IApplicationController controller, IDataBlockProvider dataBlockProvider) : base(view, controller)
        {
            _dataBlockProvider = dataBlockProvider;
            View.PropertiesUpdated += (s, a) =>
            {
                _dataBlockProvider.UpdateXml(a.Name, a.NewXml);
            };
        }

        void ICommand<EditCollectionBlockCommandArgs>.Execute(EditCollectionBlockCommandArgs commandData)
        {
            var cdb = commandData.CollectionDataBlockToEdit as CollectionDataBlock;
            var xml = _dataBlockProvider.GetXml(cdb.Name);
            View.ShowIt(cdb.Name, xml);
        }
    }
}
