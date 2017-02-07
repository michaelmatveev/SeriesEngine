using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class SolutionSelectorPresenter : Presenter<ISolutionSelector>,
        ICommand<SelectSolutionCommandArgs>
    {
        private readonly ISolutionProvider _solutionProvider;
        public SolutionSelectorPresenter(ISolutionProvider solutionProvider, ISolutionSelector view, IApplicationController controller) 
            : base(view, controller)
        {
            _solutionProvider = solutionProvider;
            View.SolutionChanged += (s, e) => controller.CurrentSolutionId = View.SelectedSolutionId;
        }

        void ICommand<SelectSolutionCommandArgs>.Execute(SelectSolutionCommandArgs commandData)
        {
            View.ShowIt(_solutionProvider.GetAllSolutions(), commandData.SolutionId);
        }
    }
}
