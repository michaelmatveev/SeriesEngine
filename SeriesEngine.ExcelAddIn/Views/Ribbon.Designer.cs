namespace SeriesEngine.ExcelAddIn
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.tab2 = this.Factory.CreateRibbonTab();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.buttonRefresh = this.Factory.CreateRibbonButton();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.toggleButtonShowPeriodSelector = this.Factory.CreateRibbonToggleButton();
            this.toggleButtonShowFragmetns = this.Factory.CreateRibbonToggleButton();
            this.menuFilter = this.Factory.CreateRibbonMenu();
            this.splitButtonFilters = this.Factory.CreateRibbonSplitButton();
            this.button1 = this.Factory.CreateRibbonButton();
            this.checkBox1 = this.Factory.CreateRibbonCheckBox();
            this.buttonSave = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.group2.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // tab2
            // 
            this.tab2.Groups.Add(this.group2);
            this.tab2.Groups.Add(this.group1);
            this.tab2.Label = "Загрузка данных";
            this.tab2.Name = "tab2";
            // 
            // group2
            // 
            this.group2.Items.Add(this.buttonRefresh);
            this.group2.Items.Add(this.buttonSave);
            this.group2.Label = "group2";
            this.group2.Name = "group2";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonRefresh.Label = "Обновить все";
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.ShowImage = true;
            this.buttonRefresh.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonRefresh_Click);
            // 
            // group1
            // 
            this.group1.Items.Add(this.toggleButtonShowPeriodSelector);
            this.group1.Items.Add(this.toggleButtonShowFragmetns);
            this.group1.Items.Add(this.menuFilter);
            this.group1.Items.Add(this.splitButtonFilters);
            this.group1.Label = "group1";
            this.group1.Name = "group1";
            // 
            // toggleButtonShowPeriodSelector
            // 
            this.toggleButtonShowPeriodSelector.Label = "Период";
            this.toggleButtonShowPeriodSelector.Name = "toggleButtonShowPeriodSelector";
            this.toggleButtonShowPeriodSelector.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonShowPeriodSelector_Click);
            // 
            // toggleButtonShowFragmetns
            // 
            this.toggleButtonShowFragmetns.Label = "Фрагменты";
            this.toggleButtonShowFragmetns.Name = "toggleButtonShowFragmetns";
            this.toggleButtonShowFragmetns.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonShowFragmetns_Click);
            // 
            // menuFilter
            // 
            this.menuFilter.Dynamic = true;
            this.menuFilter.Label = "Фильтр";
            this.menuFilter.Name = "menuFilter";
            // 
            // splitButtonFilters
            // 
            this.splitButtonFilters.ButtonType = Microsoft.Office.Tools.Ribbon.RibbonButtonType.ToggleButton;
            this.splitButtonFilters.Items.Add(this.button1);
            this.splitButtonFilters.Items.Add(this.checkBox1);
            this.splitButtonFilters.Label = "Выбор объектов";
            this.splitButtonFilters.Name = "splitButtonFilters";
            // 
            // button1
            // 
            this.button1.Label = "button1";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Label = "checkBox1";
            this.checkBox1.Name = "checkBox1";
            // 
            // buttonSave
            // 
            this.buttonSave.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonSave.Label = "Сохранить все";
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.ShowImage = true;
            this.buttonSave.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSave_Click);
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Tabs.Add(this.tab2);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        private Microsoft.Office.Tools.Ribbon.RibbonTab tab2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonShowPeriodSelector;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonShowFragmetns;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRefresh;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton splitButtonFilters;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuFilter;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBox1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSave;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
