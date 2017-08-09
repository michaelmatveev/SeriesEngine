using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
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
    public partial class SolutionPropertiesView : Form
    {
        public Solution CurrentColution
        {
            get
            {
                return solutionBindingSource.DataSource as Solution;
            }
            set
            {
                solutionBindingSource.DataSource = value;
            }
        }

        public void DisableModelSelection()
        {
            comboBoxModels.Enabled = false;
        }

        public SolutionPropertiesView()
        {
            InitializeComponent();
        }

        private void SolutionPropertiesView_Load(object sender, EventArgs e)
        {
            comboBoxModels.DataSource = ModelsDescription.All.Select(m => m.Name).ToList();
        }
    }
}
