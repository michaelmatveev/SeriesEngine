using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Business;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class StoredQueriesPresenter : Presenter<IStoredQueriesView>,
         ICommand<ShowStoredQueriesCommandArgs>
    {
        private readonly IStoredQueriesProvider _provider;

        public StoredQueriesPresenter(IStoredQueriesProvider provider, IStoredQueriesView view, IApplicationController controller) : base(view, controller)
        {
            _provider = provider;
            View.StoredQueriesUpdated += (s, e) => _provider.UpdateStoredQueries(View.StoredQueries);
        }

        void ICommand<ShowStoredQueriesCommandArgs>.Execute(ShowStoredQueriesCommandArgs commandData)
        {
            View.StoredQueries = _provider.GetStoredQueries();
            View.ShowIt();
        }
    }
}
