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
            Text = $"{ValuesCollection.VariableMetamodel.Name} - {ValuesCollection.Object.GetName()}";
            listViewVariable.Items.Clear();

            var items = ValuesCollection
                .ValuesForPeriod
                .OrderBy(vp => vp.Date)
                .Where(vp => vp.State != ObjectState.Deleted)
                .Select(vp => new ListViewItem(new[] { vp.Date.ToString(), vp.Value.ToString() })
                {
                    Tag = vp
                }).ToArray();

            listViewVariable.Items.AddRange(items);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var newVariable = Activator.CreateInstance(ValuesCollection.VariableMetamodel.EntityType) as PeriodVariable;
            newVariable.Date = DateTime.Now;
            newVariable.ObjectId = ValuesCollection.Object.Id;

            using (var varEditor = new VariableEditor(newVariable)
            {
                Text = ValuesCollection.VariableMetamodel.Name
            })
            {
                if (varEditor.ShowDialog() == DialogResult.OK)
                {
                    varEditor.Variable.State = ObjectState.Added;
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
                .Tag as PeriodVariable;

            using (var varEditor = new VariableEditor(selectedVariable)
            {
                Text = ValuesCollection.VariableMetamodel.Name
            })
            {
                if (varEditor.ShowDialog() == DialogResult.OK)
                {
                    selectedVariable.State = ObjectState.Modified;
                    FillValues();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var selectedItem = listViewVariable
                .SelectedItems
                .Cast<ListViewItem>()
                .Single();

            var selectedVariable = selectedItem.Tag as PeriodVariable;

            if(selectedVariable.State == ObjectState.Added)
            {
                listViewVariable.Items.Remove(selectedItem);
            }
            else
            {
                selectedVariable.State = ObjectState.Deleted;
            }
            FillValues();
        }

    }
}