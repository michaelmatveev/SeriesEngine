using SeriesEngine.Core.DataAccess;
using SeriesEngine.Msk1;
using System;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IObjectPropertiesView : IView
    {
        void ShowIt(MyObject selectedObject);
        event EventHandler ObjectRenamed;
        MyObject SelectedObject { get; }

    }
}
