using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.App;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App.CommandArgs;
using System;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class ObjectPropertiesPresenter : Presenter<IObjectPropertiesView>, 
        ICommand<RenameObjectCommandArgs>,
        ICommand<DeleteObjectCommandArgs>
    {
        private readonly IObjectProvider _objectProvider;
        public ObjectPropertiesPresenter(IObjectProvider objectProvider, IObjectPropertiesView view, IApplicationController controller) : base(view, controller)
        {
            _objectProvider = objectProvider;
            View.ObjectRenamed += (s, e) =>
            {
                objectProvider.UpdateObject(View.SelectedObject);
                Controller.Execute(new ReloadAllCommandArgs());
            };
        }

        void ICommand<DeleteObjectCommandArgs>.Execute(DeleteObjectCommandArgs commandData)
        {
            _objectProvider.DeleteObject(commandData.CurrentSelection, commandData.Solution);
            Controller.Execute(new ReloadAllCommandArgs());
        }

        void ICommand<RenameObjectCommandArgs>.Execute(RenameObjectCommandArgs commandData)
        {
            var selectedObject = _objectProvider.GetSelectedObject(commandData.CurrentSelection, commandData.Solution);
            View.ShowIt(selectedObject);
        }
    }
}
