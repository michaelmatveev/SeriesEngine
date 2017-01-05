using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class SwitchToViewEventArgs : EventArgs
    {
        public string ViewName { get; set; }
    }

    public interface IMainPane : IPanes
    {
        void SetViews(ICollection<string> viewNames, string viewToOpen);
        void InflateControl(Control control);

        event EventHandler<SwitchToViewEventArgs> SwitchToTheView;
    }
}
