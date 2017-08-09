using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Views
{
    public class SolutionEventArgs : EventArgs
    {
        public SolutionEventArgs(Solution solution)
        {
            Solution = solution;
        }
        public Solution Solution { get; private set; } 
    } 

    public interface ISolutionSelectorView : IView
    {
        void ShowIt(IEnumerable<Solution> solutions, Solution selectedSolution);
        void Refresh(IEnumerable<Solution> solutions, Solution selectedSolution);
        event EventHandler SolutionChanged;

        Solution SelectedSolution { get; }

        event EventHandler<SolutionEventArgs> NewSolution;
        event EventHandler<SolutionEventArgs> EditSolution;
        event EventHandler<SolutionEventArgs> DeleteSolution;

    }
}
