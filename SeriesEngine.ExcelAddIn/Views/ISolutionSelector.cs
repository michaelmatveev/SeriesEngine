using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface ISolutionSelector : IView
    {
        void ShowIt(IEnumerable<Solution> solutions, int selectedSolution);
        event EventHandler SolutionChanged;
        int SelectedSolutionId { get; }
    }
}
