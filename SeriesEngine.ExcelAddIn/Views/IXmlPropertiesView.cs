using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class PropertiesUpdatedEventArgs
    {
        public string Name { get; set; }
        public string NewXml { get; set; }
    }

    public interface IXmlPropertiesView : IView
    {
        void ShowIt(string name, string format);
        event EventHandler<PropertiesUpdatedEventArgs> PropertiesUpdated;
        string StoredQueryText { get; set; }
    }
}
