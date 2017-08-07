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
            this.groupData = this.Factory.CreateRibbonGroup();
            this.groupDataBlocks = this.Factory.CreateRibbonGroup();
            this.groupObject = this.Factory.CreateRibbonGroup();
            this.groupVariable = this.Factory.CreateRibbonGroup();
            this.label1 = this.Factory.CreateRibbonLabel();
            this.buttonGroup1 = this.Factory.CreateRibbonButtonGroup();
            this.buttonSolution = this.Factory.CreateRibbonButton();
            this.buttonDisconnect = this.Factory.CreateRibbonButton();
            this.buttonRefresh = this.Factory.CreateRibbonButton();
            this.buttonMerge = this.Factory.CreateRibbonButton();
            this.buttonSave = this.Factory.CreateRibbonButton();
            this.buttonAddDataBlock = this.Factory.CreateRibbonButton();
            this.toggleButtonShowPane = this.Factory.CreateRibbonToggleButton();
            this.menuStoredQueries = this.Factory.CreateRibbonMenu();
            this.buttonEditStoredQueries = this.Factory.CreateRibbonButton();
            this.splitButtonSamples = this.Factory.CreateRibbonSplitButton();
            this.buttonSampleMainObject = this.Factory.CreateRibbonButton();
            this.buttonSampleMainObject2 = this.Factory.CreateRibbonButton();
            this.buttonSampleSecond = this.Factory.CreateRibbonButton();
            this.buttonSampleButtonCurcuit = this.Factory.CreateRibbonButton();
            this.buttonIntegralAct = this.Factory.CreateRibbonButton();
            this.buttonIntegralAct2 = this.Factory.CreateRibbonButton();
            this.buttonRenameObject = this.Factory.CreateRibbonButton();
            this.buttonDeleteObject = this.Factory.CreateRibbonButton();
            this.buttonEdit = this.Factory.CreateRibbonButton();
            this.toggleButtonBeginOfPeriod = this.Factory.CreateRibbonToggleButton();
            this.toggleButtonEndOfPeriod = this.Factory.CreateRibbonToggleButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.tab1.SuspendLayout();
            this.tabCustom.SuspendLayout();
            this.groupConnect.SuspendLayout();
            this.groupData.SuspendLayout();
            this.groupDataBlocks.SuspendLayout();
            this.groupObject.SuspendLayout();
            this.groupVariable.SuspendLayout();
            this.buttonGroup1.SuspendLayout();
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
            this.tabCustom.Groups.Add(this.groupVariable);
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
            // groupData
            // 
            this.groupData.Items.Add(this.buttonRefresh);
            this.groupData.Items.Add(this.buttonMerge);
            this.groupData.Items.Add(this.buttonSave);
            this.groupData.Label = "Данные";
            this.groupData.Name = "groupData";
            // 
            // groupDataBlocks
            // 
            this.groupDataBlocks.Items.Add(this.buttonAddDataBlock);
            this.groupDataBlocks.Items.Add(this.toggleButtonShowPane);
            this.groupDataBlocks.Items.Add(this.separator1);
            this.groupDataBlocks.Items.Add(this.menuStoredQueries);
            this.groupDataBlocks.Items.Add(this.splitButtonSamples);
            this.groupDataBlocks.Label = "Запросы";
            this.groupDataBlocks.Name = "groupDataBlocks";
            // 
            // groupObject
            // 
            this.groupObject.Items.Add(this.buttonRenameObject);
            this.groupObject.Items.Add(this.buttonDeleteObject);
            this.groupObject.Label = "Объект";
            this.groupObject.Name = "groupObject";
            // 
            // groupVariable
            // 
            this.groupVariable.Items.Add(this.buttonEdit);
            this.groupVariable.Items.Add(this.label1);
            this.groupVariable.Items.Add(this.buttonGroup1);
            this.groupVariable.Label = "Переменная";
            this.groupVariable.Name = "groupVariable";
            // 
            // label1
            // 
            this.label1.Label = "Показывать значения";
            this.label1.Name = "label1";
            this.label1.Visible = false;
            // 
            // buttonGroup1
            // 
            this.buttonGroup1.Items.Add(this.toggleButtonBeginOfPeriod);
            this.buttonGroup1.Items.Add(this.toggleButtonEndOfPeriod);
            this.buttonGroup1.Name = "buttonGroup1";
            this.buttonGroup1.Visible = false;
            // 
            // buttonSolution
            // 
            this.buttonSolution.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonSolution.Image = global::SeriesEngine.ExcelAddIn.Properties.Resources.connect;
            this.buttonSolution.Label = "Подключиться";
            this.buttonSolution.Name = "buttonSolution";
            this.buttonSolution.ShowImage = true;
            this.buttonSolution.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSolution_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonDisconnect.Image = global::SeriesEngine.ExcelAddIn.Properties.Resources.disconnect;
            this.buttonDisconnect.Label = "Отключиться";
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.ShowImage = true;
            this.buttonDisconnect.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonDisconnect_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonRefresh.Image = global::SeriesEngine.ExcelAddIn.Properties.Resources.table_import;
            this.buttonRefresh.Label = "Загрузить";
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.ShowImage = true;
            this.buttonRefresh.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonRefresh_Click);
            // 
            // buttonMerge
            // 
            this.buttonMerge.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonMerge.Image = global::SeriesEngine.ExcelAddIn.Properties.Resources.data_table;
            this.buttonMerge.Label = "Объеденить";
            this.buttonMerge.Name = "buttonMerge";
            this.buttonMerge.ShowImage = true;
            this.buttonMerge.Visible = false;
            this.buttonMerge.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonMerge_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonSave.Image = global::SeriesEngine.ExcelAddIn.Properties.Resources.table_export;
            this.buttonSave.Label = "Сохранить";
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.ShowImage = true;
            this.buttonSave.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSave_Click);
            // 
            // buttonAddDataBlock
            // 
            this.buttonAddDataBlock.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.buttonAddDataBlock.Image = global::SeriesEngine.ExcelAddIn.Properties.Resources.table_add;
            this.buttonAddDataBlock.Label = "Вставить новый";
            this.buttonAddDataBlock.Name = "buttonAddDataBlock";
            this.buttonAddDataBlock.ShowImage = true;
            this.buttonAddDataBlock.Visible = false;
            this.buttonAddDataBlock.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAddDataBlock_Click);
            // 
            // toggleButtonShowPane
            // 
            this.toggleButtonShowPane.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.toggleButtonShowPane.Image = global::SeriesEngine.ExcelAddIn.Properties.Resources.gear_in;
            this.toggleButtonShowPane.Label = "Параметры";
            this.toggleButtonShowPane.Name = "toggleButtonShowPane";
            this.toggleButtonShowPane.ShowImage = true;
            this.toggleButtonShowPane.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.toggleButtonShowPane_Click);
            // 
            // menuStoredQueries
            // 
            this.menuStoredQueries.Dynamic = true;
            this.menuStoredQueries.Items.Add(this.buttonEditStoredQueries);
            this.menuStoredQueries.Label = "Вставить запрос";
            this.menuStoredQueries.Name = "menuStoredQueries";
            this.menuStoredQueries.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.menuStoredQueries_ItemsLoading);
            // 
            // buttonEditStoredQueries
            // 
            this.buttonEditStoredQueries.Label = "Управление запросами...";
            this.buttonEditStoredQueries.Name = "buttonEditStoredQueries";
            this.buttonEditStoredQueries.ShowImage = true;
            this.buttonEditStoredQueries.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonEditStoredQueries_Click);
            // 
            // splitButtonSamples
            // 
            this.splitButtonSamples.Items.Add(this.buttonSampleMainObject);
            this.splitButtonSamples.Items.Add(this.buttonSampleMainObject2);
            this.splitButtonSamples.Items.Add(this.buttonSampleSecond);
            this.splitButtonSamples.Items.Add(this.buttonSampleButtonCurcuit);
            this.splitButtonSamples.Items.Add(this.buttonIntegralAct);
            this.splitButtonSamples.Items.Add(this.buttonIntegralAct2);
            this.splitButtonSamples.Label = "Вставить пример";
            this.splitButtonSamples.Name = "splitButtonSamples";
            this.splitButtonSamples.Visible = false;
            // 
            // buttonSampleMainObject
            // 
            this.buttonSampleMainObject.Label = "Регион - Потребитель - Договор - Объект - Точка";
            this.buttonSampleMainObject.Name = "buttonSampleMainObject";
            this.buttonSampleMainObject.ShowImage = true;
            this.buttonSampleMainObject.Tag = "TestGrid1";
            this.buttonSampleMainObject.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSample_Click);
            // 
            // buttonSampleMainObject2
            // 
            this.buttonSampleMainObject2.Label = "Регион - Потребитель - Договор - Объект - Точка - Прибор учета";
            this.buttonSampleMainObject2.Name = "buttonSampleMainObject2";
            this.buttonSampleMainObject2.ShowImage = true;
            this.buttonSampleMainObject2.Tag = "TestGrid3";
            this.buttonSampleMainObject2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSample_Click);
            // 
            // buttonSampleSecond
            // 
            this.buttonSampleSecond.Label = "Поставщик - Договор - Точка";
            this.buttonSampleSecond.Name = "buttonSampleSecond";
            this.buttonSampleSecond.ShowImage = true;
            this.buttonSampleSecond.Tag = "TestGrid2";
            this.buttonSampleSecond.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSample_Click);
            // 
            // buttonSampleButtonCurcuit
            // 
            this.buttonSampleButtonCurcuit.Label = "Сетевая компания - Договор - Точка";
            this.buttonSampleButtonCurcuit.Name = "buttonSampleButtonCurcuit";
            this.buttonSampleButtonCurcuit.ShowImage = true;
            this.buttonSampleButtonCurcuit.Tag = "TestGrid4";
            this.buttonSampleButtonCurcuit.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSample_Click);
            // 
            // buttonIntegralAct
            // 
            this.buttonIntegralAct.Label = "Интегральный акт (договор 1001014-ЭН)";
            this.buttonIntegralAct.Name = "buttonIntegralAct";
            this.buttonIntegralAct.ShowImage = true;
            this.buttonIntegralAct.Tag = "IntegralAct";
            this.buttonIntegralAct.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSample_Click);
            // 
            // buttonIntegralAct2
            // 
            this.buttonIntegralAct2.Label = "Интегральный акт 2";
            this.buttonIntegralAct2.Name = "buttonIntegralAct2";
            this.buttonIntegralAct2.ShowImage = true;
            this.buttonIntegralAct2.Tag = "IntegralAct2";
            this.buttonIntegralAct2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonSample_Click);
            // 
            // buttonRenameObject
            // 
            this.buttonRenameObject.Image = global::SeriesEngine.ExcelAddIn.Properties.Resources.textfield_rename;
            this.buttonRenameObject.Label = "Переименовать";
            this.buttonRenameObject.Name = "buttonRenameObject";
            this.buttonRenameObject.ShowImage = true;
            this.buttonRenameObject.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonRenameObject_Click);
            // 
            // buttonDeleteObject
            // 
            this.buttonDeleteObject.Image = global::SeriesEngine.ExcelAddIn.Properties.Resources.delete;
            this.buttonDeleteObject.Label = "Удалить";
            this.buttonDeleteObject.Name = "buttonDeleteObject";
            this.buttonDeleteObject.ShowImage = true;
            this.buttonDeleteObject.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonDeleteObject_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Image = global::SeriesEngine.ExcelAddIn.Properties.Resources.application_form_edit;
            this.buttonEdit.Label = "Изменить";
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.ShowImage = true;
            this.buttonEdit.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonEdit_Click);
            // 
            // toggleButtonBeginOfPeriod
            // 
            this.toggleButtonBeginOfPeriod.Label = "на начало периода";
            this.toggleButtonBeginOfPeriod.Name = "toggleButtonBeginOfPeriod";
            this.toggleButtonBeginOfPeriod.Visible = false;
            // 
            // toggleButtonEndOfPeriod
            // 
            this.toggleButtonEndOfPeriod.Checked = true;
            this.toggleButtonEndOfPeriod.Label = "на конец периода";
            this.toggleButtonEndOfPeriod.Name = "toggleButtonEndOfPeriod";
            this.toggleButtonEndOfPeriod.Visible = false;
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
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
            this.groupVariable.ResumeLayout(false);
            this.groupVariable.PerformLayout();
            this.buttonGroup1.ResumeLayout(false);
            this.buttonGroup1.PerformLayout();
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
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRenameObject;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonDeleteObject;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupVariable;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonEdit;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSampleSecond;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSampleMainObject2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSampleButtonCurcuit;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel label1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButtonGroup buttonGroup1;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonBeginOfPeriod;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton toggleButtonEndOfPeriod;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonIntegralAct;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonIntegralAct2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonMerge;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu menuStoredQueries;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonEditStoredQueries;
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
