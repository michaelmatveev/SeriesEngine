using SeriesEngine.Core.DataAccess;
using System;

namespace SeriesEngine.ExcelAddIn.Views
{
    public enum ObjectPropertiesViewMode
    {
        Delete,
        Rename
    } 

    public interface IObjectPropertiesView : IView
    {
        void ShowIt(MyObject selectedObject, ObjectPropertiesViewMode viewMode);
        event EventHandler RenameConfirmed;
        event EventHandler DeleteConfirmed;
        MyObject SelectedObject { get; }
    }
}
