using LazyCache;
using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;
using System.Linq;
using Solution = SeriesEngine.Core.DataAccess.Solution;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ObjectCache : IObjectCache
    {
        public readonly IAppCache _cache;
        private readonly TimeSpan _refreshSpan;

        public ObjectCache()
        {
            _cache = new CachingService();
            _refreshSpan = TimeSpan.FromMinutes(10);
        }

        public ICollection<string> GetObjectsOfType(Solution solution, string type)
        {
            var key = $"{solution.Id}:{type}";
            var query = $"SELECT Name FROM {solution.MetaModelName}.{type}s WHERE SolutionId = {solution.Id}";
            return _cache.GetOrAdd(key, () => GetObjectNames(query), _refreshSpan);
        }

        private static ICollection<string> GetObjectNames(string query)
        {
            var result = new List<string>();
            for(int i = 0; i < 100000; i++)
            {
                result.Add($"item{i}");
            }
            return result;

            //using (var context = new Model1())
            //{
            //    return context.Database.SqlQuery<string>(query).ToList();
            //}
        }

    }
}
