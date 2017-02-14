using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeriesEngine.Msk1;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Views
{
    public partial class ObjectProperties : Form, IObjectPropertiesView
    {
        public ObjectProperties()
        {
            InitializeComponent();
        }

        private MyObject _selectedObject;
        public MyObject SelectedObject => _selectedObject;
 
        public event EventHandler ObjectRenamed;

        protected void OnObjectRenamed()
        {
            ObjectRenamed?.Invoke(this, EventArgs.Empty);
        }

        public void ShowIt(MyObject selectedObject)
        {
            _selectedObject = selectedObject;
            textBoxObjectName.DataBindings.Add("Text", _selectedObject, "Name");

            if (ShowDialog() == DialogResult.OK)
            {
                OnObjectRenamed();
            }
        }

    }
}
