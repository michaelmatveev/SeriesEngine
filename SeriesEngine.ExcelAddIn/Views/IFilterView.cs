using SeriesEngine.ExcelAddIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class FilterUpdatedArgs : EventArgs
    {
        public string FilterString;
    } 

    public interface IFilterView : IPanes
    {
        event EventHandler<FilterUpdatedArgs> FilterUpdated;
        void RefreshFilter(NetworkTree selectedNetwork, string currentRoute);
    }
}
