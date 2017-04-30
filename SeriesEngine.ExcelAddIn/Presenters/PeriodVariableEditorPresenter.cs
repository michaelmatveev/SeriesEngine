using SeriesEngine.ExcelAddIn.Views;
using System.Linq;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.Core.DataAccess;
using System;
using System.Globalization;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class PeriodVariableEditorPresenter : Presenter<IPeriodVariableView>,
        ICommand<EditPeriodVariableCommandArg>
    {
        private readonly IObjectProvider _objectProvider;
        private ExcelCurrentSelection _currentSelection;

        public PeriodVariableEditorPresenter(IObjectProvider objectProvider, IPeriodVariableView view, IApplicationController controller) : base(view, controller)
        {
            _objectProvider = objectProvider;
            View.EditVariableCompleted += (s, e) =>
            {
                var values = View.VariablesToShow;
                _objectProvider.ChangeData(values.NetworkId, values.ValuesForPeriod);

                var curVal = values
                    .ValuesForPeriod
                    .OrderBy(v => v.Date)
                    .ThenBy(vp => vp.Id == 0 ? int.MaxValue : vp.Id)
                    .Where(v => v.State != ObjectState.Deleted)
                    .LastOrDefault()
                    ?.Value;

                _currentSelection.Value = curVal;//Convert.ToString(curVal, CultureInfo.CurrentCulture);
            };
        }

        void ICommand<EditPeriodVariableCommandArg>.Execute(EditPeriodVariableCommandArg commandData)
        {
            _currentSelection = commandData.CurrentSelection as ExcelCurrentSelection;
            var variablesValues = _objectProvider.GetSelectedPeriodVaraible(_currentSelection, commandData.Solution);
            View.VariablesToShow = variablesValues;
            View.ShowIt();
        }

    }
}