using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class PaneControl : UserControl
    {
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

        public void ShowIt()
        {
            _embedder.Embed(this, _paneCaption);
        }
    }
}
