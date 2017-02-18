using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.App;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App.CommandArgs;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class ObjectPropertiesPresenter : Presenter<IObjectPropertiesView>, ICommand<RenameObjectCommandArgs>
    {
        private readonly IObjectProvider _objectProvider;
        public ObjectPropertiesPresenter(IObjectProvider objectProvider, IObjectPropertiesView view, IApplicationController controller) : base(view, controller)
        {
            _objectProvider = objectProvider;
            View.ObjectRenamed += (s, e) => objectProvider.UpdateObject(View.SelectedObject);
        }

        void ICommand<RenameObjectCommandArgs>.Execute(RenameObjectCommandArgs commandData)
        {
            View.ShowIt(_objectProvider.GetSelectedObject(commandData.CurrentSelection, commandData.Solution));
        }
    }
}
