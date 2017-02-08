using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface ISolutionSelector : IView
    {
        void ShowIt(IEnumerable<Solution> solutions, Solution selectedSolution);
        event EventHandler SolutionChanged;
        Solution SelectedSolution { get; }
    }
}
