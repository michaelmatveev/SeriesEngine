namespace SeriesEngine.ExcelAddIn.Views
{
    partial class FragmentProperties
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            SeriesEngine.ExcelAddIn.Models.Period period2 = new SeriesEngine.ExcelAddIn.Models.Period();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCommon = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxSheet = new System.Windows.Forms.ComboBox();
            this.textBoxCell = new System.Windows.Forms.TextBox();
            this.tabPageVariable = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxObjectTypes = new System.Windows.Forms.ComboBox();
            this.comboBoxVariables = new System.Windows.Forms.ComboBox();
            this.comboBoxKind = new System.Windows.Forms.ComboBox();
            this.labelCollectionName = new System.Windows.Forms.Label();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxShowIntervals = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxInterval = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonIntervalsByRows = new System.Windows.Forms.RadioButton();
            this.radioButtonIntervalsByColumns = new System.Windows.Forms.RadioButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxUseCommonPeriod = new System.Windows.Forms.CheckBox();
            this.numericUpDownShift = new System.Windows.Forms.NumericUpDown();
            this.comboBoxShiftInterval = new System.Windows.Forms.ComboBox();
            this.checkBoxUseShift = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.periodSelectorCustom = new SeriesEngine.ExcelAddIn.Views.PeriodSelector();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageCommon.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPageVariable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShift)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Dock = System.Windows.Forms.DockStyle.Left;
            label8.Location = new System.Drawing.Point(3, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(57, 26);
            label8.TabIndex = 0;
            label8.Text = "Название";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = System.Windows.Forms.DockStyle.Left;
            label9.Location = new System.Drawing.Point(3, 26);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(32, 27);
            label9.TabIndex = 1;
            label9.Text = "Лист";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Dock = System.Windows.Forms.DockStyle.Left;
            label10.Location = new System.Drawing.Point(3, 53);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(44, 26);
            label10.TabIndex = 2;
            label10.Text = "Ячейка";
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCommon);
            this.tabControl.Controls.Add(this.tabPageVariable);
            this.tabControl.Controls.Add(this.tabPageOutput);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(10, 3);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(359, 410);
            this.tabControl.TabIndex = 1;
            // 
            // tabPageCommon
            // 
            this.tabPageCommon.Controls.Add(this.tableLayoutPanel2);
            this.tabPageCommon.Location = new System.Drawing.Point(4, 22);
            this.tabPageCommon.Name = "tabPageCommon";
            this.tabPageCommon.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCommon.Size = new System.Drawing.Size(351, 384);
            this.tabPageCommon.TabIndex = 0;
            this.tabPageCommon.Text = "Общие";
            this.tabPageCommon.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(label8, 0, 0);
            this.tableLayoutPanel2.Controls.Add(label9, 0, 1);
            this.tableLayoutPanel2.Controls.Add(label10, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxSheet, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCell, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(345, 378);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxName.Location = new System.Drawing.Point(148, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(194, 20);
            this.textBoxName.TabIndex = 3;
            // 
            // comboBoxSheet
            // 
            this.comboBoxSheet.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSheet.FormattingEnabled = true;
            this.comboBoxSheet.Location = new System.Drawing.Point(148, 29);
            this.comboBoxSheet.Name = "comboBoxSheet";
            this.comboBoxSheet.Size = new System.Drawing.Size(194, 21);
            this.comboBoxSheet.TabIndex = 4;
            // 
            // textBoxCell
            // 
            this.textBoxCell.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxCell.Location = new System.Drawing.Point(148, 56);
            this.textBoxCell.Name = "textBoxCell";
            this.textBoxCell.Size = new System.Drawing.Size(194, 20);
            this.textBoxCell.TabIndex = 5;
            // 
            // tabPageVariable
            // 
            this.tabPageVariable.Controls.Add(this.tableLayoutPanel1);
            this.tabPageVariable.Location = new System.Drawing.Point(4, 22);
            this.tabPageVariable.Name = "tabPageVariable";
            this.tabPageVariable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVariable.Size = new System.Drawing.Size(351, 384);
            this.tabPageVariable.TabIndex = 1;
            this.tabPageVariable.Text = "Переменная";
            this.tabPageVariable.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxObjectTypes, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxVariables, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxKind, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelCollectionName, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 378);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 27);
            this.label2.TabIndex = 15;
            this.label2.Text = "Тип объектов";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 27);
            this.label4.TabIndex = 3;
            this.label4.Text = "Переменная";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(3, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 27);
            this.label6.TabIndex = 5;
            this.label6.Text = "Точность";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxObjectTypes
            // 
            this.comboBoxObjectTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxObjectTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxObjectTypes.FormattingEnabled = true;
            this.comboBoxObjectTypes.Location = new System.Drawing.Point(148, 28);
            this.comboBoxObjectTypes.Name = "comboBoxObjectTypes";
            this.comboBoxObjectTypes.Size = new System.Drawing.Size(194, 21);
            this.comboBoxObjectTypes.TabIndex = 8;
            this.comboBoxObjectTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxObjectTypes_SelectedIndexChanged);
            // 
            // comboBoxVariables
            // 
            this.comboBoxVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVariables.FormattingEnabled = true;
            this.comboBoxVariables.Location = new System.Drawing.Point(148, 55);
            this.comboBoxVariables.Name = "comboBoxVariables";
            this.comboBoxVariables.Size = new System.Drawing.Size(194, 21);
            this.comboBoxVariables.TabIndex = 10;
            // 
            // comboBoxKind
            // 
            this.comboBoxKind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKind.FormattingEnabled = true;
            this.comboBoxKind.Location = new System.Drawing.Point(148, 82);
            this.comboBoxKind.Name = "comboBoxKind";
            this.comboBoxKind.Size = new System.Drawing.Size(194, 21);
            this.comboBoxKind.TabIndex = 12;
            // 
            // labelCollectionName
            // 
            this.labelCollectionName.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelCollectionName, 2);
            this.labelCollectionName.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelCollectionName.Location = new System.Drawing.Point(3, 0);
            this.labelCollectionName.Name = "labelCollectionName";
            this.labelCollectionName.Size = new System.Drawing.Size(62, 25);
            this.labelCollectionName.TabIndex = 0;
            this.labelCollectionName.Text = "Коллекция";
            this.labelCollectionName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.tableLayoutPanel3);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(351, 384);
            this.tabPageOutput.TabIndex = 2;
            this.tabPageOutput.Text = "Вывод данных";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.checkBoxShowIntervals, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.comboBoxInterval, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(345, 378);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // checkBoxShowIntervals
            // 
            this.checkBoxShowIntervals.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.checkBoxShowIntervals, 2);
            this.checkBoxShowIntervals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxShowIntervals.Location = new System.Drawing.Point(3, 107);
            this.checkBoxShowIntervals.Name = "checkBoxShowIntervals";
            this.checkBoxShowIntervals.Size = new System.Drawing.Size(339, 17);
            this.checkBoxShowIntervals.TabIndex = 8;
            this.checkBoxShowIntervals.Text = "Показывать интервалы";
            this.checkBoxShowIntervals.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 27);
            this.label5.TabIndex = 0;
            this.label5.Text = "Данные за каждый";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(3, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 25);
            this.label7.TabIndex = 2;
            this.label7.Text = "Временные интервалы";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxInterval
            // 
            this.comboBoxInterval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterval.FormattingEnabled = true;
            this.comboBoxInterval.Location = new System.Drawing.Point(148, 3);
            this.comboBoxInterval.Name = "comboBoxInterval";
            this.comboBoxInterval.Size = new System.Drawing.Size(194, 21);
            this.comboBoxInterval.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.radioButtonIntervalsByRows);
            this.panel1.Controls.Add(this.radioButtonIntervalsByColumns);
            this.panel1.Location = new System.Drawing.Point(148, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(97, 46);
            this.panel1.TabIndex = 9;
            // 
            // radioButtonIntervalsByRows
            // 
            this.radioButtonIntervalsByRows.AutoSize = true;
            this.radioButtonIntervalsByRows.Checked = true;
            this.radioButtonIntervalsByRows.Location = new System.Drawing.Point(3, 3);
            this.radioButtonIntervalsByRows.Name = "radioButtonIntervalsByRows";
            this.radioButtonIntervalsByRows.Size = new System.Drawing.Size(85, 17);
            this.radioButtonIntervalsByRows.TabIndex = 5;
            this.radioButtonIntervalsByRows.TabStop = true;
            this.radioButtonIntervalsByRows.Text = "По строкам";
            this.radioButtonIntervalsByRows.UseVisualStyleBackColor = true;
            // 
            // radioButtonIntervalsByColumns
            // 
            this.radioButtonIntervalsByColumns.AutoSize = true;
            this.radioButtonIntervalsByColumns.Location = new System.Drawing.Point(3, 26);
            this.radioButtonIntervalsByColumns.Name = "radioButtonIntervalsByColumns";
            this.radioButtonIntervalsByColumns.Size = new System.Drawing.Size(91, 17);
            this.radioButtonIntervalsByColumns.TabIndex = 6;
            this.radioButtonIntervalsByColumns.Text = "По столбцам";
            this.radioButtonIntervalsByColumns.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(351, 384);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Период";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.checkBoxUseCommonPeriod, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.numericUpDownShift, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxShiftInterval, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.checkBoxUseShift, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label11, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.periodSelectorCustom, 0, 4);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 6;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(345, 378);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // checkBoxUseCommonPeriod
            // 
            this.checkBoxUseCommonPeriod.AutoSize = true;
            this.checkBoxUseCommonPeriod.Checked = true;
            this.checkBoxUseCommonPeriod.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel4.SetColumnSpan(this.checkBoxUseCommonPeriod, 2);
            this.checkBoxUseCommonPeriod.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxUseCommonPeriod.Location = new System.Drawing.Point(3, 3);
            this.checkBoxUseCommonPeriod.Name = "checkBoxUseCommonPeriod";
            this.checkBoxUseCommonPeriod.Size = new System.Drawing.Size(174, 17);
            this.checkBoxUseCommonPeriod.TabIndex = 0;
            this.checkBoxUseCommonPeriod.Text = "Использовать общий период";
            this.checkBoxUseCommonPeriod.UseVisualStyleBackColor = true;
            this.checkBoxUseCommonPeriod.CheckedChanged += new System.EventHandler(this.checkBoxUseCommonPeriod_CheckedChanged);
            // 
            // numericUpDownShift
            // 
            this.numericUpDownShift.Dock = System.Windows.Forms.DockStyle.Right;
            this.numericUpDownShift.Location = new System.Drawing.Point(99, 49);
            this.numericUpDownShift.Name = "numericUpDownShift";
            this.numericUpDownShift.Size = new System.Drawing.Size(43, 20);
            this.numericUpDownShift.TabIndex = 2;
            // 
            // comboBoxShiftInterval
            // 
            this.comboBoxShiftInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShiftInterval.FormattingEnabled = true;
            this.comboBoxShiftInterval.Location = new System.Drawing.Point(148, 49);
            this.comboBoxShiftInterval.Name = "comboBoxShiftInterval";
            this.comboBoxShiftInterval.Size = new System.Drawing.Size(121, 21);
            this.comboBoxShiftInterval.TabIndex = 3;
            // 
            // checkBoxUseShift
            // 
            this.checkBoxUseShift.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.checkBoxUseShift, 2);
            this.checkBoxUseShift.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxUseShift.Location = new System.Drawing.Point(3, 26);
            this.checkBoxUseShift.Name = "checkBoxUseShift";
            this.checkBoxUseShift.Size = new System.Drawing.Size(262, 17);
            this.checkBoxUseShift.TabIndex = 1;
            this.checkBoxUseShift.Text = "Со смещением относительно общего периода";
            this.checkBoxUseShift.UseVisualStyleBackColor = true;
            this.checkBoxUseShift.CheckedChanged += new System.EventHandler(this.checkBoxUseShift_CheckedChanged);
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Location = new System.Drawing.Point(3, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 27);
            this.label11.TabIndex = 4;
            this.label11.Text = "Специальный период";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // periodSelectorCustom
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.periodSelectorCustom, 2);
            this.periodSelectorCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.periodSelectorCustom.Enabled = false;
            this.periodSelectorCustom.Location = new System.Drawing.Point(3, 103);
            this.periodSelectorCustom.Name = "periodSelectorCustom";
            period2.From = new System.DateTime(2016, 9, 17, 23, 57, 55, 44);
            period2.Till = new System.DateTime(2016, 9, 17, 23, 57, 55, 49);
            this.periodSelectorCustom.SelectedPeriod = period2;
            this.periodSelectorCustom.Size = new System.Drawing.Size(339, 150);
            this.periodSelectorCustom.TabIndex = 5;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.buttonCancel);
            this.flowLayoutPanel2.Controls.Add(this.buttonOK);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 410);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(359, 29);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(281, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(200, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // FragmentProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 439);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.flowLayoutPanel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FragmentProperties";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Свойства фрагмента";
            this.tabControl.ResumeLayout(false);
            this.tabPageCommon.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPageVariable.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPageOutput.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShift)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCommon;
        private System.Windows.Forms.TabPage tabPageVariable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxObjectTypes;
        private System.Windows.Forms.ComboBox comboBoxVariables;
        private System.Windows.Forms.ComboBox comboBoxKind;
        private System.Windows.Forms.Label labelCollectionName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxSheet;
        private System.Windows.Forms.TextBox textBoxCell;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxInterval;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.CheckBox checkBoxUseCommonPeriod;
        private System.Windows.Forms.NumericUpDown numericUpDownShift;
        private System.Windows.Forms.ComboBox comboBoxShiftInterval;
        private System.Windows.Forms.CheckBox checkBoxUseShift;
        private System.Windows.Forms.Label label11;
        private PeriodSelector periodSelectorCustom;
        private System.Windows.Forms.CheckBox checkBoxShowIntervals;
        private System.Windows.Forms.RadioButton radioButtonIntervalsByRows;
        private System.Windows.Forms.RadioButton radioButtonIntervalsByColumns;
        private System.Windows.Forms.Panel panel1;
    }
}
