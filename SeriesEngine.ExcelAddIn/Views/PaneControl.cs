using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class PaneControl : UserControl, IPanes
    {
        public event EventHandler PaneClosed;
        protected readonly string _paneCaption;
        private readonly IViewEmbedder _embedder;

        public PaneControl()
        {
        }

        public PaneControl(IViewEmbedder embedder, string caption)
        {
            _embedder = embedder;
            _paneCaption = caption;
        }

        public void ShowIt(PaneLocation defaultLocation = PaneLocation.Right)
        {
            _embedder.Embed(this, _paneCaption, defaultLocation);            
        }

        public void HideIt()
        {
            _embedder.Release(this);
        }

        public void ParentPaneClosed()
        {
            PaneClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}
