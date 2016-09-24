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
        public FragmentProperties(IList<string> sheets)
        {
            InitializeComponent();
            comboBoxSheet.DataSource = sheets;
            comboBoxInterval.DataSource = comboBoxShiftInterval.DataSource = Enum.GetValues(typeof(TimeInterval));
        }

        public event EventHandler FragmentChanged;
        public Fragment Fragment { get; set; }
        //public IEnumerable<Network> Networks { get; set; }

        public void ShowIt()
        {            
            textBoxName.DataBindings.Add(nameof(textBoxName.Text), Fragment, nameof(Fragment.Name));
            comboBoxSheet.DataBindings.Add(nameof(comboBoxInterval.SelectedItem), Fragment, nameof(Fragment.Sheet));
            textBoxCell.DataBindings.Add(nameof(textBoxCell.Text), Fragment, nameof(Fragment.Cell));

            labelCollectionName.Text = Fragment.SourceCollection.Name;
            
            comboBoxInterval.DataBindings.Add(nameof(comboBoxInterval.SelectedItem), Fragment, nameof(Fragment.Interval));
            radioButtonIntervalsByRows.DataBindings.Add(nameof(radioButtonIntervalsByRows.Checked), Fragment, nameof(Fragment.IntervalsByRows));
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
    }
}
