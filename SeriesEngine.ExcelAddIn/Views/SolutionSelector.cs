using SeriesEngine.Msk1;
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

        public void ShowIt(IEnumerable<Solution> solutions, int selectedSolution)
        {
            var items = solutions.Select(s => new ListViewItem(new[] { s.Name, s.Description })
            {
                Tag = s,
                Selected = s.Id == selectedSolution
            }).ToArray();

            listViewSolutions.Items.AddRange(items);

            if (ShowDialog() == DialogResult.OK)
            {
                OnSolutionChanged();
            }
        }

        public int SelectedSolutionId
        {
            get
            {
                var item = listViewSolutions.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
                if(item == null)
                {
                    return 0;
                }

                return ((Solution)item.Tag).Id;
            }

        }

        public event EventHandler SolutionChanged;

        protected void OnSolutionChanged()
        {
            SolutionChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
