using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface ISolutionSelectorView : IView
    {
        void ShowIt(IEnumerable<Solution> solutions, Solution selectedSolution);
        event EventHandler SolutionChanged;
        Solution SelectedSolution { get; }
    }
}
