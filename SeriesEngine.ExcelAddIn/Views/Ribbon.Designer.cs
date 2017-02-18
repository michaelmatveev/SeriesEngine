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
            this.groupConnect = this.Factory.CreateRibbonGroup();
            this.buttonSolution = this.Factory.CreateRibbonButton();
            this.buttonDisconnect = this.Factory.CreateRibbonButton();
            this.groupData = this.Factory.CreateRibbonGroup();
            this.buttonRefresh = this.Factory.CreateRibbonButton();
            this.buttonSave = this.Factory.CreateRibbonButton();
            this.groupDataBlocks = this.Factory.CreateRibbonGroup();
            this.buttonAddDataBlock = this.Factory.CreateRibbonButton();
            this.toggleButtonShowPane = this.Factory.CreateRibbonToggleButton();
            this.splitButtonSamples = this.Factory.CreateRibbonSplitButton();
            this.buttonSampleMainObject = this.Factory.CreateRibbonButton();
            this.groupObject = this.Factory.CreateRibbonGroup();
            this.label1 = this.Factory.CreateRibbonLabel();
            this.buttonRenameObject = this.Factory.CreateRibbonButton();
            this.buttonDeleteObject = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.tabCustom.SuspendLayout();
            this.groupConnect.SuspendLayout();
            this.groupData.SuspendLayout();
            this.groupDataBlocks.SuspendLayout();
            this.groupObject.SuspendLayout();
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
            this.tabCustom.Groups.Add(this.groupConnect);
            this.tabCustom.Groups.Add(this.groupData);
            this.tabCustom.Groups.Add(this.groupDataBlocks);
            this.tabCustom.Groups.Add(this.groupObject);
            this.tabCustom.Label = "Загрузка данных";
            this.tabCustom.Name = "tabCustom";
            // 
            // groupConnect
            // 
            this.groupConnect.Items.Add(this.buttonSolution);
            this.groupConnect.Items.Add(this.buttonDisconnect);
            this.groupConnect.Label = "Подключение";
            this.groupConnect.Name = "groupConnect";
            // 
            // buttonSolution
            // 
            this.buttonSolution.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonSolution.Label = "Подключиться";
            this.buttonSolution.Name = "buttonSolution";
            this.buttonSolution.ShowImage = true;
            this.buttonSolution.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSolution_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonDisconnect.Label = "Отключиться";
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.ShowImage = true;
            this.buttonDisconnect.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonDisconnect_Click);
            // 
            // groupData
            // 
            this.groupData.Items.Add(this.buttonRefresh);
            this.groupData.Items.Add(this.buttonSave);
            this.groupData.Label = "Данные";
            this.groupData.Name = "groupData";
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
            // groupDataBlocks
            // 
            this.groupDataBlocks.Items.Add(this.buttonAddDataBlock);
            this.groupDataBlocks.Items.Add(this.toggleButtonShowPane);
            this.groupDataBlocks.Items.Add(this.splitButtonSamples);
            this.groupDataBlocks.Label = "Блок данных";
            this.groupDataBlocks.Name = "groupDataBlocks";
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
            // groupObject
            // 
            this.groupObject.Items.Add(this.label1);
            this.groupObject.Items.Add(this.buttonRenameObject);
            this.groupObject.Items.Add(this.buttonDeleteObject);
            this.groupObject.Label = "Объект";
            this.groupObject.Name = "groupObject";
            // 
            // label1
            // 
            this.label1.Label = "label1";
            this.label1.Name = "label1";
            // 
            // buttonRenameObject
            // 
            this.buttonRenameObject.Label = "Переименовать";
            this.buttonRenameObject.Name = "buttonRenameObject";
            this.buttonRenameObject.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonRenameObject_Click);
            // 
            // buttonDeleteObject
            // 
            this.buttonDeleteObject.Label = "Удалить";
            this.buttonDeleteObject.Name = "buttonDeleteObject";
            this.buttonDeleteObject.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonDeleteObject_Click);
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
            this.groupConnect.ResumeLayout(false);
            this.groupConnect.PerformLayout();
            this.groupData.ResumeLayout(false);
            this.groupData.PerformLayout();
            this.groupDataBlocks.ResumeLayout(false);
            this.groupDataBlocks.PerformLayout();
            this.groupObject.ResumeLayout(false);
            this.groupObject.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        private Microsoft.Office.Tools.Ribbon.RibbonTab tabCustom;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupDataBlocks;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonShowPane;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupData;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRefresh;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSave;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddDataBlock;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSampleMainObject;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton splitButtonSamples;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSolution;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupConnect;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonDisconnect;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupObject;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel label1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRenameObject;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonDeleteObject;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
