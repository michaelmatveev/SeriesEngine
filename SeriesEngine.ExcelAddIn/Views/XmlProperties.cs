using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class XmlProperties : Form, IXmlPropertiesView
    {
        public XmlProperties()
        {
            InitializeComponent();
        }

        public event EventHandler<PropertiesUpdatedEventArgs> PropertiesUpdated;
        void IXmlPropertiesView.ShowIt(string name, string dataBlock)
        {
            Text = name;
            textBoxDataBlock.Text = dataBlock;
            if (ShowDialog() == DialogResult.OK)
            {
                PropertiesUpdated?.Invoke(this,
                    new PropertiesUpdatedEventArgs
                    {
                        Name = name,
                        NewXml = textBoxDataBlock.Text
                    });
            }
        }
    }
}
