using System;
using System.Windows.Forms;
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
            if(selectedObject == null)
            {
                MessageBox.Show("Данный объект нельзя переименовать.");
                return;
            }

            if(selectedObject.NodeId == -1)
            {
                MessageBox.Show($"Объект {selectedObject.Name} возможно уже был переименован. Попробуйте обновить данные.");
                return;
            }

            _selectedObject = selectedObject;
            textBoxObjectName.DataBindings.Add("Text", _selectedObject, "Name");

            if (ShowDialog() == DialogResult.OK)
            {
                OnObjectRenamed();
            }
        }

    }
}
