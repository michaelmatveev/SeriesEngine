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
            this.tabCustom = this.Factory.CreateRibbonTab();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.buttonSolution = this.Factory.CreateRibbonButton();
            this.buttonRefresh = this.Factory.CreateRibbonButton();
            this.buttonSave = this.Factory.CreateRibbonButton();
            this.buttonAddDataBlock = this.Factory.CreateRibbonButton();
            this.toggleButtonShowPane = this.Factory.CreateRibbonToggleButton();
            this.splitButtonSamples = this.Factory.CreateRibbonSplitButton();
            this.buttonSampleMainObject = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.tabCustom.SuspendLayout();
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
            // tabCustom
            // 
            this.tabCustom.Groups.Add(this.group2);
            this.tabCustom.Groups.Add(this.group1);
            this.tabCustom.Label = "Загрузка данных";
            this.tabCustom.Name = "tabCustom";
            // 
            // group2
            // 
            this.group2.Items.Add(this.buttonSolution);
            this.group2.Items.Add(this.separator1);
            this.group2.Items.Add(this.buttonRefresh);
            this.group2.Items.Add(this.buttonSave);
            this.group2.Label = "Данные";
            this.group2.Name = "group2";
            // 
            // group1
            // 
            this.group1.Items.Add(this.buttonAddDataBlock);
            this.group1.Items.Add(this.toggleButtonShowPane);
            this.group1.Items.Add(this.splitButtonSamples);
            this.group1.Label = "Блок данных";
            this.group1.Name = "group1";
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // buttonSolution
            // 
            this.buttonSolution.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonSolution.Label = "Подключиться";
            this.buttonSolution.Name = "buttonSolution";
            this.buttonSolution.ShowImage = true;
            this.buttonSolution.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSolution_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonRefresh.Label = "Обновить все";
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.ShowImage = true;
            this.buttonRefresh.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonRefresh_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonSave.Label = "Сохранить все";
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.ShowImage = true;
            this.buttonSave.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSave_Click);
            // 
            // buttonAddDataBlock
            // 
            this.buttonAddDataBlock.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonAddDataBlock.Label = "Вставить новый";
            this.buttonAddDataBlock.Name = "buttonAddDataBlock";
            this.buttonAddDataBlock.ShowImage = true;
            this.buttonAddDataBlock.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAddDataBlock_Click);
            // 
            // toggleButtonShowPane
            // 
            this.toggleButtonShowPane.Label = "Параметры";
            this.toggleButtonShowPane.Name = "toggleButtonShowPane";
            this.toggleButtonShowPane.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonShowPane_Click);
            // 
            // splitButtonSamples
            // 
            this.splitButtonSamples.Items.Add(this.buttonSampleMainObject);
            this.splitButtonSamples.Label = "Вставить пример";
            this.splitButtonSamples.Name = "splitButtonSamples";
            // 
            // buttonSampleMainObject
            // 
            this.buttonSampleMainObject.Label = "Основные объекты";
            this.buttonSampleMainObject.Name = "buttonSampleMainObject";
            this.buttonSampleMainObject.ShowImage = true;
            this.buttonSampleMainObject.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSample_Click);
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Tabs.Add(this.tabCustom);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.tabCustom.ResumeLayout(false);
            this.tabCustom.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        private Microsoft.Office.Tools.Ribbon.RibbonTab tabCustom;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonShowPane;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRefresh;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSave;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddDataBlock;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSampleMainObject;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton splitButtonSamples;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSolution;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
