using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class VariableEditor : Form
    {
        public VariableEditor(PeriodVariable variable, Variable metamodel, Period allowedPeriod)
        {
            InitializeComponent();
            Variable = variable;
            Text = metamodel.Name;
            dateTimePicker.MinDate = allowedPeriod.FromDate;
            dateTimePicker.MaxDate = allowedPeriod.TillDate.AddSeconds(-1);
            dateTimePicker.DataBindings.Add(nameof(dateTimePicker.Value), Variable, nameof(Variable.Date));
            var varBinding = textBoxValue.DataBindings.Add(nameof(textBoxValue.Text), Variable, nameof(Variable.Value), true);
            varBinding.Parse += (s, e) =>
            {
                e.Value = metamodel.Parse(e.Value.ToString());
            };
        }

        public PeriodVariable Variable { get; private set; }
    }
}
