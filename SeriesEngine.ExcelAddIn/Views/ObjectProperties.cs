﻿using System;
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

        private EditorObject _selectedObject;
        public EditorObject SelectedObject => _selectedObject;
 
        public event EventHandler RenameConfirmed;

        protected void OnRenameConfirmed()
        {
            RenameConfirmed?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler DeleteConfirmed;

        protected void OnDeleteConfirmed()
        {
            DeleteConfirmed?.Invoke(this, EventArgs.Empty);
        }

        public void ShowIt(EditorObject selectedObject, ObjectPropertiesViewMode viewMode)
        {
            if(selectedObject == null)
            {
                var message = "Объект не выбран. Выберите ячейку, содержащую имя объекта";
                MessageBox.Show(message);
                return;
            }

            if(selectedObject.NodeId == -1)
            {
                var message = viewMode == ObjectPropertiesViewMode.Delete ?
                    $"Объект '{selectedObject.Name}' уже удален или не был сохранен." :
                    $"Объект '{selectedObject.Name}' уже переименован или не был сохранен.";

                MessageBox.Show($"{message}{Environment.NewLine}Попробуйте обновить данные.");
                return;
            }

            _selectedObject = selectedObject;
            if (viewMode == ObjectPropertiesViewMode.Delete)
            {
                var message = $"Вы действительно хотите удалить объект '{selectedObject.Name}'?{Environment.NewLine}" +
                    $"ВНИМАНИЕ: Это приведет к удалению всех связанных с ним данных и удалению объекта из всех блоков данных.{Environment.NewLine}" +
                    $"Для удаления объекта только из блока данных удалите все строки из таблицы, содержащие его имя.";
                
                if(MessageBox.Show(message, ViewNames.ApplicationCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    OnDeleteConfirmed();
                }
            }
            else
            {
                textBoxObjectName.DataBindings.Add("Text", _selectedObject, "Name");

                if (ShowDialog() == DialogResult.OK)
                {
                    OnRenameConfirmed();
                }
            }
        }

    }
}