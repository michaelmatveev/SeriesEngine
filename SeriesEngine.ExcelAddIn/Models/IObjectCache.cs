using SeriesEngine.Core.DataAccess;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IObjectCache
    {
        string GetObjectsOfType(Solution solution, string type);
    }
}
