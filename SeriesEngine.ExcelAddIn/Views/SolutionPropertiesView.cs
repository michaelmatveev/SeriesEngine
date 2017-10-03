using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class SolutionPropertiesView : Form
    {
        public Solution CurrentSolution
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

        private readonly Func<string> _solutionNameErrorProvider;

        public SolutionPropertiesView(Func<string> solutioNameErrorProvider)
        {
            _solutionNameErrorProvider = solutioNameErrorProvider;
            InitializeComponent();
        }

        private void SolutionPropertiesView_Load(object sender, EventArgs e)
        {
           comboBoxModels.DataSource = ModelsDescription
                .All
                .Where(m => m.Visible)
                .Select(m => m.Name).ToList();
        }

        private bool ValidateAllData()
        {
            var message = _solutionNameErrorProvider();
            if(!string.IsNullOrEmpty(message))
            {
                errorProvider.SetError(textBoxName, message);
                return false;    
            }

            errorProvider.SetError(textBoxName, string.Empty);
            return true;            
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!ValidateAllData())
            {
                DialogResult = DialogResult.None;
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(textBoxName, string.Empty);
        }
    }
}
