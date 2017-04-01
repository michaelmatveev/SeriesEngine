using SeriesEngine.Core.DataAccess;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class VariableEditor : Form
    {
        public VariableEditor(PeriodVariable variable, Period allowedPeriod)
        {
            InitializeComponent();
            Variable = variable;
            dateTimePicker.MinDate = allowedPeriod.From;
            dateTimePicker.MaxDate = allowedPeriod.Till;
            dateTimePicker.DataBindings.Add(nameof(dateTimePicker.Value), Variable, nameof(Variable.Date));
            textBoxValue.DataBindings.Add(nameof(textBoxValue.Text), Variable, nameof(Variable.Value), true);
        }

        public PeriodVariable Variable { get; private set; }
    }
}
