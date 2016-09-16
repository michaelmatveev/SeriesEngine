namespace SeriesEngine.ExcelAddIn.Views
{
    partial class Fragments
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
            this.listViewFragments = new System.Windows.Forms.ListView();
            this.columnHeaderCell = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.listViewFragments, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(338, 415);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listViewFragments
            // 
            this.listViewFragments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderCell});
            this.listViewFragments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFragments.FullRowSelect = true;
            this.listViewFragments.Location = new System.Drawing.Point(3, 3);
            this.listViewFragments.Name = "listViewFragments";
            this.listViewFragments.Size = new System.Drawing.Size(332, 300);
            this.listViewFragments.TabIndex = 0;
            this.listViewFragments.UseCompatibleStateImageBehavior = false;
            this.listViewFragments.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderCell
            // 
            this.columnHeaderCell.Text = "Ячейка";
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Имя";
            // 
            // Fragments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Fragments";
            this.Size = new System.Drawing.Size(338, 415);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listViewFragments;
        private System.Windows.Forms.ColumnHeader columnHeaderCell;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
    }
}
