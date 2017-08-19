using SeriesEngine.Core.DataAccess;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface ISolutionProvider
    {
        IEnumerable<Solution> GetAllSolutions();
        Solution GetSolutionById(int solutionId);
        void InsertSolution(Solution solution);
        void UpdateSolution(Solution solution);
        void DeleteSolution(Solution solution);
        bool ValidateSolutionName(Solution solutionToCheck, out string errorMessage);

    }
}
