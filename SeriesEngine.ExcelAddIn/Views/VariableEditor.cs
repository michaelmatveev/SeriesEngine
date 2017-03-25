using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class VariableEditor : Form
    {
        public VariableEditor(EditorPeriodVariable variable)
        {
            InitializeComponent();
            Variable = variable;
            dateTimePicker.DataBindings.Add(nameof(dateTimePicker.Value), Variable, nameof(Variable.Period));
            textBoxValue.DataBindings.Add(nameof(textBoxValue.Text), Variable, nameof(Variable.Value));
        }

        public EditorPeriodVariable Variable { get; private set; }
    }
}
