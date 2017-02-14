using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.App;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.App.CommandArgs;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class ObjectPropertiesPresenter : Presenter<IObjectPropertiesView>, ICommand<GetObjectCommandArgs>
    {
        private readonly IObjectProvider _objectProvider;
        public ObjectPropertiesPresenter(IObjectProvider objectProvider, IObjectPropertiesView view, IApplicationController controller) : base(view, controller)
        {
            _objectProvider = objectProvider;
            View.ObjectRenamed += (s, e) => objectProvider.UpdateObject(View.SelectedObject);
        }

        void ICommand<GetObjectCommandArgs>.Execute(GetObjectCommandArgs commandData)
        {
            View.ShowIt(_objectProvider.GetSelectedObject());
        }
    }
}
