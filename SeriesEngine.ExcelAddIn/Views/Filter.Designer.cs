namespace SeriesEngine.ExcelAddIn.Views
{
    partial class Filter
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
            this.breadCrumbs = new SeriesEngine.ExcelAddIn.Views.BreadCrumbs();
            this.SuspendLayout();
            // 
            // breadCrumbs
            // 
            this.breadCrumbs.BackColor = System.Drawing.Color.Transparent;
            this.breadCrumbs.BreadCrumbItems = null;
            this.breadCrumbs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.breadCrumbs.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.breadCrumbs.Location = new System.Drawing.Point(0, 0);
            this.breadCrumbs.Name = "breadCrumbs";
            this.breadCrumbs.Size = new System.Drawing.Size(227, 25);
            this.breadCrumbs.StartButtonCaption = "Press this button to start";
            this.breadCrumbs.TabIndex = 0;
            this.breadCrumbs.Text = "breadCrumbs1";
            this.breadCrumbs.SizeChanged += new System.EventHandler(this.breadCrumbs1_SizeChanged);
            // 
            // Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.breadCrumbs);
            this.Name = "Filter";
            this.Size = new System.Drawing.Size(227, 25);
            this.SizeChanged += new System.EventHandler(this.Filter_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BreadCrumbs breadCrumbs;
    }
}
