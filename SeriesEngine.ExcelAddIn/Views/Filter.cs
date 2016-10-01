using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class Filter : PaneControl, IFilterView
    {
        public Filter(IViewEmbedder embedder) : base(embedder, "Фильтр")
        {
            InitializeComponent();
        }

        private void breadCrumbs1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Filter_SizeChanged(object sender, EventArgs e)
        {
            //if (Globals.ThisAddIn..Height <= 150)
            //{
            //SendKeys.Send("{ESC}");

            //    Globals.ThisAddIn.ctp.Height = 150;

            //}
        }
    }
}
