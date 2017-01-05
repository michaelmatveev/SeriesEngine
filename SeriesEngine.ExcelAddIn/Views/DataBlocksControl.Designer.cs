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
            this.treeViewSheetsAndBlocks = new System.Windows.Forms.TreeView();
            this.linkLabelDeleteDataCollection = new System.Windows.Forms.LinkLabel();
            this.linkLabelAddDataBlock = new System.Windows.Forms.LinkLabel();
            this.linkLabelCopyDataBlockCollection = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.treeViewSheetsAndBlocks, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.linkLabelDeleteDataCollection, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.linkLabelAddDataBlock, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.linkLabelCopyDataBlockCollection, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(338, 415);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // treeViewSheetsAndBlocks
            // 
            this.treeViewSheetsAndBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSheetsAndBlocks.Location = new System.Drawing.Point(3, 3);
            this.treeViewSheetsAndBlocks.Name = "treeViewSheetsAndBlocks";
            this.treeViewSheetsAndBlocks.Size = new System.Drawing.Size(332, 349);
            this.treeViewSheetsAndBlocks.TabIndex = 5;
            this.treeViewSheetsAndBlocks.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewFragments_NodeMouseDoubleClick);
            // 
            // linkLabelDeleteDataCollection
            // 
            this.linkLabelDeleteDataCollection.AutoSize = true;
            this.linkLabelDeleteDataCollection.Location = new System.Drawing.Point(3, 395);
            this.linkLabelDeleteDataCollection.Name = "linkLabelDeleteDataCollection";
            this.linkLabelDeleteDataCollection.Size = new System.Drawing.Size(148, 13);
            this.linkLabelDeleteDataCollection.TabIndex = 6;
            this.linkLabelDeleteDataCollection.TabStop = true;
            this.linkLabelDeleteDataCollection.Text = "Удалить коллекцию блоков";
            this.linkLabelDeleteDataCollection.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDeleteDataCollection_LinkClicked);
            // 
            // linkLabelAddDataBlock
            // 
            this.linkLabelAddDataBlock.AutoSize = true;
            this.linkLabelAddDataBlock.Location = new System.Drawing.Point(3, 375);
            this.linkLabelAddDataBlock.Name = "linkLabelAddDataBlock";
            this.linkLabelAddDataBlock.Size = new System.Drawing.Size(124, 13);
            this.linkLabelAddDataBlock.TabIndex = 7;
            this.linkLabelAddDataBlock.TabStop = true;
            this.linkLabelAddDataBlock.Text = "Добавить блок данных";
            // 
            // linkLabelCopyDataBlockCollection
            // 
            this.linkLabelCopyDataBlockCollection.AutoSize = true;
            this.linkLabelCopyDataBlockCollection.Location = new System.Drawing.Point(3, 355);
            this.linkLabelCopyDataBlockCollection.Name = "linkLabelCopyDataBlockCollection";
            this.linkLabelCopyDataBlockCollection.Size = new System.Drawing.Size(165, 13);
            this.linkLabelCopyDataBlockCollection.TabIndex = 8;
            this.linkLabelCopyDataBlockCollection.TabStop = true;
            this.linkLabelCopyDataBlockCollection.Text = "Копировать коллекцию блоков";
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
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView treeViewSheetsAndBlocks;
        private System.Windows.Forms.LinkLabel linkLabelDeleteDataCollection;
        private System.Windows.Forms.LinkLabel linkLabelAddDataBlock;
        private System.Windows.Forms.LinkLabel linkLabelCopyDataBlockCollection;
    }
}
