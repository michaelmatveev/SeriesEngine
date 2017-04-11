using LazyCache;
using Microsoft.Office.Tools.Excel;
using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;
using System.Linq;
using Solution = SeriesEngine.Core.DataAccess.Solution;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ObjectCache : IObjectCache
    {
        private readonly IAppCache _cache;
        private readonly Workbook _workbook;
        private readonly TimeSpan _refreshSpan;

        public ObjectCache(Workbook workbook)
        {
            _workbook = workbook;
            _cache = new CachingService();
            _refreshSpan = TimeSpan.FromMinutes(10);
        }

        public string GetObjectsOfType(Solution solution, string type)
        {
            var key = $"{solution.Id}:{type}";
            var query = $"SELECT Name FROM {solution.MetaModelName}.{type}s WHERE SolutionId = {solution.Id}";

            var hiddenSheetName = $"Hidden{solution.Id}";
            var hiddenSheet = _workbook
                .Worksheets
                .OfType<Microsoft.Office.Interop.Excel.Worksheet>()
                .FirstOrDefault(w => w.Name == hiddenSheetName);

            if (hiddenSheet == null)
            {
                hiddenSheet = _workbook.Worksheets.Add();
                hiddenSheet.Name = hiddenSheetName;
                hiddenSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden;
            }

            var column = GetColumn(type);
            return _cache.GetOrAdd(key, () => GetObjectNames(query, hiddenSheet, column), _refreshSpan);
        }

        private string GetColumn(string type)
        {
            switch(type)
            {
                case "Region": return "A";
                case "Consumer": return "B";
                case "Contract": return "C";
                case "ConsumerObject": return "D";
                case "Point": return "E";                
            }
            return "";
        } 

        private static string GetObjectNames(string query, Microsoft.Office.Interop.Excel.Worksheet sheet, string column)
        {
            using (var context = new Model1())
            {
                var names = context.Database.SqlQuery<string>(query).ToList();
                var r = sheet.UsedRange.get_Range($"{column}:{column}");
                r.EntireColumn.Clear();
                var array = new object[names.Count, 1];
                var i = 0;
                foreach(var name in names)
                {
                    array[i++, 0] = name;
                }
                sheet.get_Range($"{column}1:{column}{i}").Value2 = array;
                return $"='{sheet.Name}'!${column}$1:${column}${i}";
            }
        }

    }
}