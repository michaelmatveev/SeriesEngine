using System;
using System.Collections.Generic;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.Core;
using System.Linq;
using System.Data.Entity.Validation;

namespace SeriesEngine.ExcelAddIn.Business
{
    public class StoredQueriesProvider : IStoredQueriesProvider
    {
        public IList<StoredQuery> GetStoredQueries(string modelName = null)
        {
            using (var context = ModelsDescription.GetModel())
            {
                if (modelName == null)
                {
                    return context
                        .StoredQueries
                        .ToList();
                }
                else
                {
                    return context
                        .StoredQueries
                        .Where(q => q.ModelName == modelName)
                        .ToList();
                }
            }
        }

        public void UpdateStoredQueries(IList<StoredQuery> queries)
        {
            try
            {
                using (var context = ModelsDescription.GetModel())
                {
                    foreach (var q in queries)
                    {
                        if (q.State == ObjectState.Added)
                        {
                            context.StoredQueries.Add(q);
                        }
                        else
                        {
                            context.StoredQueries.Attach(q);
                        }
                    }
                    context.FixState();
                    context.Database.Log = x => System.Diagnostics.Debug.WriteLine(x);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors.SelectMany(s => s.ValidationErrors);
                var message = string.Join(", ", errors.Select(e => e.ErrorMessage));
                throw new InvalidOperationException(message);
            }
        }
    }
}
