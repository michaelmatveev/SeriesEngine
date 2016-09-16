using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IPanes : IView
    {
        event EventHandler PaneClosed;
        void ShowIt();
        void HideIt();
    }
}
