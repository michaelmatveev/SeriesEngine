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
        public IEnumerable<Network> Networks { get; set; }

        public void ShowIt()
        {
            //textBoxName.DataBindings.Clear();
            textBoxName.DataBindings.Add("Text", Fragment, "Name");

            comboBoxNetworks.DisplayMember = nameof(Network.Name);
            comboBoxNetworks.DataSource = Networks.ToList();
            
            if (ShowDialog() == DialogResult.OK)
            {
                FragmentChanged?.Invoke(this, EventArgs.Empty);
            }


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
