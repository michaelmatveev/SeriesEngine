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

        public void ShowIt(IEnumerable<Solution> solutions)
        {
            var items = solutions.Select(s => new ListViewItem(new[] { s.Name, s.Description })
            {
                Tag = s
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

            set
            {
                listViewSolutions.SelectedItems.Clear();
                var item = listViewSolutions.SelectedItems.Cast<ListViewItem>().FirstOrDefault(i => ((Solution)i.Tag).Id == value);
                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }

        public event EventHandler SolutionChanged;

        protected void OnSolutionChanged()
        {
            SolutionChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
