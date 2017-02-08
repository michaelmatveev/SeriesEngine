using SeriesEngine.Core.DataAccess;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface ISolutionProvider
    {
        IEnumerable<Solution> GetAllSolutions();
        Solution GetSolutionById(int solutionId);
    }
}
