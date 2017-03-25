using SeriesEngine.Core.DataAccess;
using System;
using System.Data;
using System.Linq;
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

        public EditPeriodVariables ValuesCollection { get; set; }

        public event EventHandler EditVariableCompleted;

        void IPeriodVariableView.ShowIt()
        {
            FillValues();
            if (ShowDialog() == DialogResult.OK)
            {
                EditVariableCompleted?.Invoke(this, EventArgs.Empty); 
            }
        }
        
        private void FillValues()
        {
            Text = $"{ValuesCollection.VariableMetamodel.Name} - {ValuesCollection.ObjectName}";
            listViewVariable.Items.Clear();

            var items = ValuesCollection
                .ValuesForPeriod
                .OrderBy(vp => vp.Period)
                .Select(vp => new ListViewItem(new[] { vp.Period.ToString(), vp.Value.ToString() })
                {
                    Tag = vp
                }).ToArray();

            listViewVariable.Items.AddRange(items);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var newVariable = new EditorPeriodVariable
            {
                Period = DateTime.Now
            };

            using (var varEditor = new VariableEditor(newVariable)
            {
                Text = ValuesCollection.VariableMetamodel.Name
            })
            {
                if (varEditor.ShowDialog() == DialogResult.OK)
                {
                    ValuesCollection.ValuesForPeriod.Add(varEditor.Variable);
                    FillValues();
                }
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            var selectedVariable = listViewVariable
                .SelectedItems
                .Cast<ListViewItem>()
                .Single()
                .Tag as EditorPeriodVariable;

            using (var varEditor = new VariableEditor(selectedVariable)
            {
                Text = ValuesCollection.VariableMetamodel.Name
            })
            {
                if (varEditor.ShowDialog() == DialogResult.OK)
                {
                    FillValues();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }
    }
}