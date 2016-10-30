namespace SeriesEngine.ExcelAddIn.Views
{
    partial class MainPaneControl
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelToPlace = new System.Windows.Forms.Panel();
            this.comboBoxGoTo = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.panelToPlace, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.comboBoxGoTo, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.14516F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(261, 496);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // panelToPlace
            // 
            this.panelToPlace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelToPlace.Location = new System.Drawing.Point(3, 30);
            this.panelToPlace.Name = "panelToPlace";
            this.panelToPlace.Size = new System.Drawing.Size(255, 463);
            this.panelToPlace.TabIndex = 0;
            // 
            // comboBoxGoTo
            // 
            this.comboBoxGoTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxGoTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGoTo.FormattingEnabled = true;
            this.comboBoxGoTo.Location = new System.Drawing.Point(3, 3);
            this.comboBoxGoTo.Name = "comboBoxGoTo";
            this.comboBoxGoTo.Size = new System.Drawing.Size(255, 21);
            this.comboBoxGoTo.TabIndex = 1;
            this.comboBoxGoTo.SelectedIndexChanged += new System.EventHandler(this.comboBoxGoTo_SelectedIndexChanged);
            // 
            // MainPaneControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MainPaneControl";
            this.Size = new System.Drawing.Size(261, 496);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panelToPlace;
        private System.Windows.Forms.ComboBox comboBoxGoTo;
    }
}
