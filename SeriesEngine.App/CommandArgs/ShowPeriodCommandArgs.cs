using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.App.CommandArgs
{
    public class ShowCustomPaneCommandArgs
    {
        public bool IsVisible { get; set; }
        public string ViewNameToOpen { get; set; }
    }
}
