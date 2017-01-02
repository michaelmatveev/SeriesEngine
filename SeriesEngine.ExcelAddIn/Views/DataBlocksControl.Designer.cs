namespace SeriesEngine.ExcelAddIn.Views
{
    partial class DataBlocksControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabelAddFragment = new System.Windows.Forms.LinkLabel();
            this.linkLabelCopyFragment = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.treeViewSheetsAndBlocks = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.treeViewSheetsAndBlocks, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(338, 415);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.linkLabel1);
            this.flowLayoutPanel2.Controls.Add(this.linkLabelAddFragment);
            this.flowLayoutPanel2.Controls.Add(this.linkLabelCopyFragment);
            this.flowLayoutPanel2.Controls.Add(this.linkLabel4);
            this.flowLayoutPanel2.Controls.Add(this.linkLabel3);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(13, 354);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(312, 48);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.flowLayoutPanel2.SetFlowBreak(this.linkLabel1, true);
            this.linkLabel1.Location = new System.Drawing.Point(3, 0);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(116, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Добавить коллекцию";
            // 
            // linkLabelAddFragment
            // 
            this.linkLabelAddFragment.AutoSize = true;
            this.linkLabelAddFragment.Location = new System.Drawing.Point(3, 16);
            this.linkLabelAddFragment.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.linkLabelAddFragment.Name = "linkLabelAddFragment";
            this.linkLabelAddFragment.Size = new System.Drawing.Size(110, 13);
            this.linkLabelAddFragment.TabIndex = 4;
            this.linkLabelAddFragment.TabStop = true;
            this.linkLabelAddFragment.Text = "Добавить фрагмент";
            this.linkLabelAddFragment.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAddFragment_LinkClicked);
            // 
            // linkLabelCopyFragment
            // 
            this.linkLabelCopyFragment.AutoSize = true;
            this.flowLayoutPanel2.SetFlowBreak(this.linkLabelCopyFragment, true);
            this.linkLabelCopyFragment.Location = new System.Drawing.Point(119, 16);
            this.linkLabelCopyFragment.Name = "linkLabelCopyFragment";
            this.linkLabelCopyFragment.Size = new System.Drawing.Size(120, 13);
            this.linkLabelCopyFragment.TabIndex = 7;
            this.linkLabelCopyFragment.TabStop = true;
            this.linkLabelCopyFragment.Text = "Копировать фрагмент";
            this.linkLabelCopyFragment.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCopyFragment_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(3, 32);
            this.linkLabel4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(58, 13);
            this.linkLabel4.TabIndex = 6;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Изменить";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(67, 32);
            this.linkLabel3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(50, 13);
            this.linkLabel3.TabIndex = 5;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Удалить";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // treeViewSheetsAndBlocks
            // 
            this.treeViewSheetsAndBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSheetsAndBlocks.Location = new System.Drawing.Point(13, 13);
            this.treeViewSheetsAndBlocks.Name = "treeViewSheetsAndBlocks";
            this.treeViewSheetsAndBlocks.Size = new System.Drawing.Size(312, 335);
            this.treeViewSheetsAndBlocks.TabIndex = 5;
            this.treeViewSheetsAndBlocks.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewFragments_NodeMouseDoubleClick);
            // 
            // DataBlocksControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DataBlocksControl";
            this.Size = new System.Drawing.Size(338, 415);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TreeView treeViewSheetsAndBlocks;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabelAddFragment;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabelCopyFragment;
    }
}
