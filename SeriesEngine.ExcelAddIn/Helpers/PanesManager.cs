using Microsoft.Office.Core;
using Microsoft.Office.Tools;
using SeriesEngine.ExcelAddIn.Views;
using System.Linq;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Helpers
{
    public enum PaneLocation
    {
        Top,
        Right
    }

    public class PanesManager
    {
        private readonly CustomTaskPaneCollection _panesCollection;

        public PanesManager(CustomTaskPaneCollection panesCollection)
        {
            _panesCollection = panesCollection;
        }

        public void Embed<T>(T viewToEmbed, string caption, PaneLocation location = PaneLocation.Right)
        {
            var panel = _panesCollection.SingleOrDefault(p => p.Control == viewToEmbed as UserControl);
            if (panel == null)
            {
                panel = _panesCollection.Add(viewToEmbed as UserControl, caption);
                if(location == PaneLocation.Top)
                {
                    panel.DockPosition = MsoCTPDockPosition.msoCTPDockPositionTop;
                    panel.Height = 60;
                }
                else
                {
                    panel.Width = 315;
                }                
                panel.VisibleChanged += (s, e) =>
                {
                    if (!panel.Visible)
                    {
                        (viewToEmbed as PaneControl).ParentPaneClosed();
                    }
                };
            }
            panel.Visible = true;
        }

        public void Release<T>(T viewToRelease)
        {
            var panel = _panesCollection.SingleOrDefault(p => p.Control == viewToRelease as UserControl);
            panel.Visible = false;
        }
    }
}
