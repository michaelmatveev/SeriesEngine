using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class PeriodVariableEditorPresenter : Presenter<IPeriodVariableView>,
        ICommand<EditPeriodVariableCommandArg>
    {
        private readonly IObjectProvider _objectProvider;
        private readonly IDataBlockProvider _blockProvider;
        private ExcelCurrentSelection _currentSelection;

        public PeriodVariableEditorPresenter(IObjectProvider objectProvider, IDataBlockProvider blockProvider, IPeriodVariableView view, IApplicationController controller) : base(view, controller)
        {
            _objectProvider = objectProvider;
            _blockProvider = blockProvider;
            View.EditVariableCompleted += (s, e) =>
            {
                var values = View.ValuesCollection;
                _objectProvider.ChangeData(values.NetworkId, values.ValuesForPeriod);

                _currentSelection.Value = values
                    .ValuesForPeriod
                    .Where(v => v.State != ObjectState.Deleted)
                    .LastOrDefault(v => blockProvider.GetDefaultPeriod().Include(v.Date))
                    ?.Value
                    .ToString();
            };
        }

        void ICommand<EditPeriodVariableCommandArg>.Execute(EditPeriodVariableCommandArg commandData)
        {
            _currentSelection = commandData.CurrentSelection as ExcelCurrentSelection;
            var variablesValues = _objectProvider.GetSelectedPeriodVaraible(_currentSelection, commandData.Solution);
            View.ValuesCollection = variablesValues;
            View.ShowIt(_blockProvider.GetDefaultPeriod());
        }
    }
}
