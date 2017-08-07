using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IStoredQueriesView : IView
    {
        IList<StoredQuery> StoredQueries { get; set; }
        void ShowIt();

        event EventHandler StoredQueriesUpdated;
    }
}
