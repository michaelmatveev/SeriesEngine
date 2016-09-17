using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class FragmentProperties : Form, IFragmentPropertiesView
    {
        public FragmentProperties()
        {
            InitializeComponent();
        }

        public event EventHandler FragmentChanged;
        public Fragment Fragment { get; set; }

        public void ShowIt()
        {
            if(ShowDialog() == DialogResult.OK)
            {
                FragmentChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}
