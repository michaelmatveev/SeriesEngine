using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public enum PaneLocation
    {
        Top,
        Right
    }

    public interface IViewEmbedder
    {
        void Embed<T>(T viewToEmbed, string caption, PaneLocation location = PaneLocation.Right);
        void Release<T>(T viewToRelease);
        //event EventHandler PaneClosed;
    }
}
