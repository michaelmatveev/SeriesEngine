using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Business
{
    public interface IStoredQueriesProvider
    {
        IList<StoredQuery> GetStoredQueries(string modelName = null);
        void UpdateStoredQueries(IList<StoredQuery> queries);
    }
}
