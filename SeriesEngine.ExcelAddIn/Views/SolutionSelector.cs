using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class SolutionSelector : Form, ISolutionSelector
    {
        public SolutionSelector()
        {
            InitializeComponent();
        }

        public void ShowIt(IEnumerable<Solution> solutions, Solution selectedSolution)
        {
            var items = solutions.Select(s => new ListViewItem(new[] { s.Name, s.Description })
            {
                Tag = s,
                Selected = selectedSolution != null ? s.Id == selectedSolution.Id : false
            }).ToArray();

            listViewSolutions.Items.AddRange(items);

            if (ShowDialog() == DialogResult.OK)
            {
                OnSolutionChanged();
            }
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

        protected void OnSolutionChanged()
        {
            SolutionChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
