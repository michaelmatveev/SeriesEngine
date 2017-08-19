using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Application;
using System.Linq;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class SolutionSelector : Form, ISolutionSelectorView
    {
        public SolutionSelector()
        {
            InitializeComponent();
        }

        private void SolutionSelector_Load(object sender, EventArgs e)
        {
            var version = ApplicationDeployment.IsNetworkDeployed ?
                ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() :
                "Локальная сборка";
            labelVersion.Text = $"Версия: {version}"; 
        }

        public void ShowIt(IEnumerable<Solution> solutions, Solution selectedSolution)
        {
            Refresh(solutions, selectedSolution);
            if (ShowDialog() == DialogResult.OK)
            {
                SolutionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Refresh(IEnumerable<Solution> solutions, Solution selectedSolution)
        {
            var items = solutions.Select(s => new ListViewItem(new[] { s.Name, s.ModelName, s.Description })
            {
                Tag = s,
                Selected = selectedSolution != null ? s.Id == selectedSolution.Id : false
            }).ToArray();
            listViewSolutions.Items.Clear();
            listViewSolutions.Items.AddRange(items);
        }

        public Solution SelectedSolution
        {
            get
            {
                var item = listViewSolutions.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
                return (Solution)item?.Tag;
            }

        }

        public event EventHandler SolutionChanged;
        public event EventHandler<SolutionEventArgs> NewSolution;
        public event EventHandler<SolutionEventArgs> EditSolution;
        public event EventHandler<SolutionEventArgs> DeleteSolution;
        public event EventHandler<SolutionEventArgs> ValidateSolution;

        private void listViewSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEdit.Enabled = buttonDelete.Enabled = listViewSolutions.SelectedItems.Count > 0;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var solution = listViewSolutions.SelectedItems[0].Tag as Solution;
            using (var form = CreatePropertiesView(solution))
            {
                form.DisableModelSelection();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    EditSolution?.Invoke(this, new SolutionEventArgs(solution));
                }
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            var solution = new Solution();
            using (var form = CreatePropertiesView(solution))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    NewSolution?.Invoke(this, new SolutionEventArgs(solution));
                }
            }
        }

        private SolutionPropertiesView CreatePropertiesView(Solution solution)
        {
            Func<string> errorProvider = () =>
            {
                var arg = new SolutionEventArgs(solution);
                ValidateSolution?.Invoke(this, arg);
                return arg.ValidationError;
            };

            var form = new SolutionPropertiesView(errorProvider) { CurrentSolution = solution };
            return form;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var solution = listViewSolutions.SelectedItems[0].Tag as Solution;
            if (MessageBox.Show($"Удалить решение '{solution.Name}'?", ViewNames.ApplicationCaption, MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                DeleteSolution?.Invoke(this, new SolutionEventArgs(solution));            
            }
        }

    }
}