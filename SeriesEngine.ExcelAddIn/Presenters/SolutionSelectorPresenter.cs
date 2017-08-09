using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class SolutionSelectorPresenter : Presenter<ISolutionSelectorView>,
        ICommand<SelectSolutionCommandArgs>
    {
        private readonly ISolutionProvider _solutionProvider;
        public SolutionSelectorPresenter(ISolutionProvider solutionProvider, ISolutionSelectorView view, IApplicationController controller) 
            : base(view, controller)
        {
            _solutionProvider = solutionProvider;
            View.SolutionChanged += (s, e) => controller.CurrentSolution = View.SelectedSolution;
            View.NewSolution += (s, e) =>
            {
                solutionProvider.InsertSolution(e.Solution);
                View.Refresh(solutionProvider.GetAllSolutions(), e.Solution);
            };
            View.EditSolution += (s, e) =>
            {
                solutionProvider.UpdateSolution(e.Solution);
                View.Refresh(solutionProvider.GetAllSolutions(), e.Solution);
            };
            View.DeleteSolution += (s, e) =>
            {
                solutionProvider.DeleteSolution(e.Solution);
                View.Refresh(solutionProvider.GetAllSolutions(), null);
            };
        }

        void ICommand<SelectSolutionCommandArgs>.Execute(SelectSolutionCommandArgs commandData)
        {
            View.ShowIt(_solutionProvider.GetAllSolutions(), commandData.Solution);
        }
    }
}
