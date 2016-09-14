using Microsoft.Office.Tools;
using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Helpers
{
    public class PanesManager : IViewEmbedder
    {
        private readonly CustomTaskPaneCollection _panesCollection;

        public PanesManager(CustomTaskPaneCollection panesCollection)
        {
            _panesCollection = panesCollection;
        }

        //public PanesManager(string panesCollection)
        //{
        //    //_panesCollection = panesCollection;
        //    var s = panesCollection;
        //}


        public void Embed<T>(T viewToEmbed, string caption)
        {
            var panel = _panesCollection.SingleOrDefault(p => p.Control == viewToEmbed as Control);
            if (panel == null)
            {
                panel = _panesCollection.Add(viewToEmbed as UserControl, caption);
            }
            panel.Visible = true;
        }

        public void Release<T>(T viewToRelease)
        {
            var panel = _panesCollection.SingleOrDefault(p => p.Control == viewToRelease as Control);
            panel.Visible = false;
        }
    }
}
