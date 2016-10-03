using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class FragmentProperties : Form, IFragmentPropertiesView
    {
        private IModelProvider _modelProvider; 

        public FragmentProperties(IList<string> sheets, IModelProvider modelProvider)
        {
            _modelProvider = modelProvider;
            InitializeComponent();
            comboBoxSheet.DataSource = sheets;
            comboBoxInterval.DataSource = comboBoxShiftInterval.DataSource = Enum.GetValues(typeof(TimeInterval));
           

            comboBoxKind.DataSource = Enum.GetValues(typeof(Kind));
        }

        public event EventHandler FragmentChanged;
        public Fragment Fragment { get; set; }

        public void ShowIt()
        {
            textBoxName.DataBindings.Add(nameof(textBoxName.Text), Fragment, nameof(Fragment.Name));
            comboBoxSheet.DataBindings.Add(nameof(comboBoxInterval.SelectedItem), Fragment, nameof(Fragment.Sheet));
            textBoxCell.DataBindings.Add(nameof(textBoxCell.Text), Fragment, nameof(Fragment.Cell));

            labelCollectionName.Text = Fragment.Parent.Name;
            comboBoxObjectTypes.DataBindings.Add(nameof(comboBoxObjectTypes.SelectedValue), Fragment, nameof(Fragment.ObjectMetamodel));//, false, DataSourceUpdateMode.OnPropertyChanged);
            comboBoxVariables.DataBindings.Add(nameof(comboBoxVariables.SelectedValue), Fragment, nameof(Fragment.VariableMetamodel));
            comboBoxKind.DataBindings.Add(nameof(comboBoxKind.SelectedItem), Fragment, nameof(Fragment.Kind));

            comboBoxObjectTypes.DisplayMember = "Name";
            comboBoxObjectTypes.ValueMember = "ObjectModel";
            var objectsMetamodels = ((CollectionFragment)Fragment.Parent)
                .SupportedModels
                .Select(om => new { Name = om.Name, ObjectModel = om }).ToList();
            comboBoxObjectTypes.DataSource = objectsMetamodels;

            comboBoxInterval.DataBindings.Add(nameof(comboBoxInterval.SelectedItem), Fragment, nameof(Fragment.Interval));
            radioButtonIntervalsByRows.DataBindings.Add(nameof(radioButtonIntervalsByRows.Checked), Fragment, nameof(Fragment.IntervalsByRows));
            radioButtonIntervalsByColumns.DataBindings.Add(nameof(radioButtonIntervalsByColumns.Checked), Fragment, nameof(Fragment.IntervalsByColumns));
            checkBoxShowIntervals.DataBindings.Add(nameof(checkBoxShowIntervals.Checked), Fragment, nameof(Fragment.ShowIntervals));

            checkBoxUseCommonPeriod.DataBindings.Add(nameof(checkBoxUseCommonPeriod.Checked), Fragment, nameof(Fragment.UseCommonPeriod));
            checkBoxUseShift.DataBindings.Add(nameof(checkBoxUseShift.Checked), Fragment, nameof(Fragment.UseShift));
            numericUpDownShift.DataBindings.Add(nameof(numericUpDownShift.Value), Fragment, nameof(Fragment.Shift));
            comboBoxShiftInterval.DataBindings.Add(nameof(comboBoxShiftInterval.SelectedItem), Fragment, nameof(Fragment.ShiftPeriod));
            periodSelectorCustom.DataBindings.Add(nameof(periodSelectorCustom.SelectedPeriod), Fragment, nameof(Fragment.CustomPeriod));

            SetUseCommonPeriodState(Fragment.UseCommonPeriod);
            SetShiftState(Fragment.UseShift);

            if (ShowDialog() == DialogResult.OK)
            {
                //Fragment.ObjectMetamodel = (ObjectMetamodel)comboBoxObjectTypes.SelectedValue;
                //Fragment.VariableMetamodel = (Variable)comboBoxVariables.SelectedValue;                
                FragmentChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void SetUseCommonPeriodState(bool state)
        {
            checkBoxUseShift.Enabled = state;
            periodSelectorCustom.Enabled = !state;
        }

        private void SetShiftState(bool state)
        {
            numericUpDownShift.Enabled = comboBoxShiftInterval.Enabled = state;
        }

        private void checkBoxUseCommonPeriod_CheckedChanged(object sender, EventArgs e)
        {
            SetUseCommonPeriodState(checkBoxUseCommonPeriod.Checked);
        }

        private void checkBoxUseShift_CheckedChanged(object sender, EventArgs e)
        {
            SetShiftState(checkBoxUseShift.Checked);
        }

        private void comboBoxObjectTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var om = (ObjectMetamodel)comboBoxObjectTypes.SelectedValue;
            
            if (om != null && om.Variables != null)
            {
                //comboBoxObjectTypes.DataBindings["SelectedValue"].WriteValue();

                comboBoxVariables.DisplayMember = "Name";
                comboBoxVariables.ValueMember = "VariableModel";
                comboBoxVariables.DataSource = om.Variables.Select(vm => new { Name = vm.Name, VariableModel = vm }).ToList();
            }
        }
    }
}
