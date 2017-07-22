using SeriesEngine.Core;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IModelProvider
    {
        IEnumerable<ObjectMetamodel> GetObjectMetamodels();
    }
}
