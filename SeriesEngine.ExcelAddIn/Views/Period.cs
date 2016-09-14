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
    public partial class Period : PaneControl, IPeriodView
    {
        public Period(IViewEmbedder embedder) : base(embedder, "Период")
        {
            InitializeComponent();
        }

    }
}
