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
        private Period _selectedPeriod;

        void IPeriodVariableView.ShowIt(Period selectedPeriod)
        {
            _selectedPeriod = selectedPeriod;
            labelPeriod.Text = $"Переменные отображаются за период {_selectedPeriod}";
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

            var sameDateGroups = ValuesCollection
               .ValuesForPeriod
               .OrderBy(vp => vp.Date)
               .OrderBy(vp => vp.Id == 0 ? Int32.MaxValue : vp.Id)
               .Where(vp => vp.State != ObjectState.Deleted)
               .GroupBy(vp => vp.Date);

            var items = ValuesCollection
                .ValuesForPeriod
                .OrderBy(vp => vp.Date)
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
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var newVariable = Activator.CreateInstance(ValuesCollection.VariableMetamodel.EntityType) as PeriodVariable;
            var nowDate = DateTime.Now.Date;

            newVariable.Date = _selectedPeriod.Include(nowDate) ? nowDate : _selectedPeriod.From;
            newVariable.ObjectId = ValuesCollection.Object.Id;

            using (var varEditor = new VariableEditor(newVariable, _selectedPeriod)
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

            using (var varEditor = new VariableEditor(selectedVariable, _selectedPeriod)
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

        //private void listViewVariable_DrawItem(object sender, DrawListViewItemEventArgs e)
        //{
        //     var inSameGroup = ValuesCollection
        //        .ValuesForPeriod
        //        .OrderBy(vp => vp.Date)
        //        .OrderBy(vp => vp.Id)
        //        .Where(vp => vp.State != ObjectState.Deleted)
        //        .GroupBy(vp => vp.Date)
        //        .Where(vg => vg.Count() > 1)
        //        .Any(vg => vg.Key.ToString() == e.Item.SubItems[0].ToString());
        //    if (inSameGroup)
        //    {
        //        e.Item.Text = "    " + e.Item.Text;
        //    }
        //    e.DrawDefault = true;
        //}
    }
}