using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class PeriodVariableEditor : Form, 
        IPeriodVariableView
    {
        public PeriodVariableEditor()
        {
            InitializeComponent();
        }

        void IPeriodVariableView.ShowIt()
        {
            if (ShowDialog() == DialogResult.OK)
            {
               // OnSolutionChanged();
            }
        }

    }
}
