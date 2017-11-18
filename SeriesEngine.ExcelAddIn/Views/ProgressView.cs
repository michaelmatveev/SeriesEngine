using SeriesEngine.ExcelAddIn.Models;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class ProgressView : Form, IProgressView
    {
        private Stopwatch _stopWath = new Stopwatch();
        public ProgressView()
        {
            InitializeComponent();
        }

        public void Start(string caption)
        {
            _stopWath.Start();
            this.Text = caption;
            this.Show();
            this.BringToFront();
        }

        public void Stop()
        {
            if (this.InvokeRequired)
            {
                var d = new Action(Stop);
                this.Invoke(d);
            }
            else
            {
                _stopWath.Reset();
                this.Hide();
            }
        }

        public void UpdateInfo(string description)
        {
            if (this.InvokeRequired)
            {
                var d = new Action<string>(UpdateInfo);
                this.Invoke(d, description);
            }
            else
            {
                labelDescription.Text = description;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            labelTime.Text = TimeSpan.FromTicks(_stopWath.ElapsedTicks).ToString(@"mm\:ss\.ff");
        }

    }
}
