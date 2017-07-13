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

        public EditPeriodVariables VariablesToShow { get; set; }

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
            Text = $"{VariablesToShow.VariableMetamodel.Name} - {VariablesToShow.Object.GetName()}";
            labelPeriod.Text = $"История изменения переменной отображаются за период {VariablesToShow.SelectedPeriod}";
            if(VariablesToShow.InitialValue != null)
            {
                labelStartValue.Text = $"Значение на начало периода: {VariablesToShow.InitialValue.Value}";
            }
            else
            {
                labelStartValue.Text = "Значение на начало периода не определено";
            }

            listViewVariable.Items.Clear();

            var sameDateGroups = VariablesToShow
               .ValuesForPeriod
               .OrderBy(vp => vp.Date)
               .ThenBy(vp => vp.Id == 0 ? int.MaxValue : vp.Id)
               .Where(vp => vp.State != ObjectState.Deleted)
               .GroupBy(vp => vp.Date);

            var items = VariablesToShow
                .ValuesForPeriod
                .OrderBy(vp => vp.Date)
                .ThenBy(vp => vp.Id == 0 ? int.MaxValue : vp.Id)
                .Where(vp => vp.State != ObjectState.Deleted)
                .Select(vp => 
                    new ListViewItem(
                        new[] 
                        {
                            (sameDateGroups.First(g => g.Key == vp.Date).ToList().IndexOf(vp) > 0 ? "   " : string.Empty) + vp.Date.ToString(),
                            vp.Value.ToString()
                        })
                    {
                        Tag = vp                    
                    })
                .ToArray();

            listViewVariable.Items.AddRange(items);
            buttonModify.Enabled = buttonDelete.Enabled = items.Any();
                 
            var lastValue = (items.LastOrDefault()?.Tag as PeriodVariable) ?? VariablesToShow.InitialValue;
            if (lastValue != null)
            {
                labelEndValue.Text = $"Значение на конец периода: {lastValue.Value}";
            }
            else
            {
                labelEndValue.Text = "Значение на конец периода не определено";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var newVariable = Activator.CreateInstance(VariablesToShow.VariableMetamodel.EntityType) as PeriodVariable;
            var nowDate = DateTime.Now.Date;

            newVariable.Date = VariablesToShow.SelectedPeriod.Include(nowDate) ? nowDate : VariablesToShow.SelectedPeriod.From;
            newVariable.ObjectId = VariablesToShow.Object.Id;

            using (var varEditor = new VariableEditor(newVariable, VariablesToShow.VariableMetamodel, VariablesToShow.SelectedPeriod))
            {
                if (varEditor.ShowDialog() == DialogResult.OK)
                {
                    varEditor.Variable.State = ObjectState.Added;
                    VariablesToShow.ValuesForPeriod.Add(varEditor.Variable);
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

            using (var varEditor = new VariableEditor(selectedVariable, VariablesToShow.VariableMetamodel, VariablesToShow.SelectedPeriod))
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