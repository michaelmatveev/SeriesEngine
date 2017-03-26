using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.App;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App.CommandArgs;

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
            View.RenameConfirmed += (s, e) =>
            {
                objectProvider.RenameObject(View.SelectedObject);
                Controller.Execute(new ReloadAllCommandArgs());
            };
            View.DeleteConfirmed += (s, e) =>
            {
                objectProvider.DeleteObject(View.SelectedObject);
                Controller.Execute(new ReloadAllCommandArgs());
            };
        }

        void ICommand<DeleteObjectCommandArgs>.Execute(DeleteObjectCommandArgs commandData)
        {
            var selectedObject = _objectProvider.GetSelectedObject(commandData.CurrentSelection, commandData.Solution);
            View.ShowIt(selectedObject, ObjectPropertiesViewMode.Delete);
        }

        void ICommand<RenameObjectCommandArgs>.Execute(RenameObjectCommandArgs commandData)
        {
            var selectedObject = _objectProvider.GetSelectedObject(commandData.CurrentSelection, commandData.Solution);
            View.ShowIt(selectedObject, ObjectPropertiesViewMode.Rename);
        }
    }
}
