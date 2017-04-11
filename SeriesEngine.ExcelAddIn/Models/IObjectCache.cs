using SeriesEngine.Core.DataAccess;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IObjectCache
    {
        ICollection<string> GetObjectsOfType(Solution solution, string type);
    }
}
