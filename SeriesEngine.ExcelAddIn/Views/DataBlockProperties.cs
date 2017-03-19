using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.Msk1;
using SeriesEngine.Core;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class DataBlockProperties : Form, IDataBlockPropertiesView
    {
        private IModelProvider _modelProvider; 

        public DataBlockProperties(IModelProvider modelProvider)
        {
            _modelProvider = modelProvider;
            InitializeComponent();
            comboBoxInterval.DataSource = comboBoxShiftInterval.DataSource = Enum.GetValues(typeof(TimeInterval));
            comboBoxKind.DataSource = Enum.GetValues(typeof(Kind));
        }

        public event EventHandler VariableBlockChanged;
        public DataBlock DataBlock { get; set; }

        public void ShowIt()
        {
            textBoxName.DataBindings.Add(nameof(textBoxName.Text), DataBlock, nameof(DataBlock.Name));

            labelCollectionName.Text = DataBlock.Parent.Name;
            comboBoxObjectTypes.DataBindings.Add(nameof(comboBoxObjectTypes.SelectedValue), DataBlock, nameof(DataBlock.ObjectMetamodel));//, false, DataSourceUpdateMode.OnPropertyChanged);
//            comboBoxVariables.DataBindings.Add(nameof(comboBoxVariables.SelectedValue), DataBlock, nameof(DataBlock.VariableMetamodel));
            comboBoxKind.DataBindings.Add(nameof(comboBoxKind.SelectedItem), DataBlock, nameof(DataBlock.Kind));

            comboBoxObjectTypes.DisplayMember = "Name";
            comboBoxObjectTypes.ValueMember = "ObjectModel";
            var objectsMetamodels = ((CollectionDataBlock)DataBlock.Parent)
                .SupportedModels
                .Select(om => new { Name = om.Name, ObjectModel = om }).ToList();
            comboBoxObjectTypes.DataSource = objectsMetamodels;

            comboBoxInterval.DataBindings.Add(nameof(comboBoxInterval.SelectedItem), DataBlock, nameof(DataBlock.Interval));
            radioButtonIntervalsByRows.DataBindings.Add(nameof(radioButtonIntervalsByRows.Checked), DataBlock, nameof(DataBlock.IntervalsByRows));
            radioButtonIntervalsByColumns.DataBindings.Add(nameof(radioButtonIntervalsByColumns.Checked), DataBlock, nameof(DataBlock.IntervalsByColumns));
            checkBoxShowIntervals.DataBindings.Add(nameof(checkBoxShowIntervals.Checked), DataBlock, nameof(DataBlock.ShowIntervals));

            checkBoxUseCommonPeriod.DataBindings.Add(nameof(checkBoxUseCommonPeriod.Checked), DataBlock, nameof(DataBlock.UseCommonPeriod));
            checkBoxUseShift.DataBindings.Add(nameof(checkBoxUseShift.Checked), DataBlock, nameof(DataBlock.UseShift));
            numericUpDownShift.DataBindings.Add(nameof(numericUpDownShift.Value), DataBlock, nameof(DataBlock.Shift));
            comboBoxShiftInterval.DataBindings.Add(nameof(comboBoxShiftInterval.SelectedItem), DataBlock, nameof(DataBlock.ShiftPeriod));
            //periodSelectorCustom.DataBindings.Add(nameof(periodSelectorCustom.SelectedPeriod), Fragment, nameof(Fragment.CustomPeriod));

            SetUseCommonPeriodState(DataBlock.UseCommonPeriod);
            SetShiftState(DataBlock.UseShift);

            if (ShowDialog() == DialogResult.OK)
            {
                //Fragment.ObjectMetamodel = (ObjectMetamodel)comboBoxObjectTypes.SelectedValue;
                //Fragment.VariableMetamodel = (Variable)comboBoxVariables.SelectedValue;                
                VariableBlockChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void SetUseCommonPeriodState(bool state)
        {
            checkBoxUseShift.Enabled = state;
            //periodSelectorCustom.Enabled = !state;
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
