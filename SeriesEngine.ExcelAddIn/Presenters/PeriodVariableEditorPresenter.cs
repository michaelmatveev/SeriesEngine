using SeriesEngine.ExcelAddIn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using SeriesEngine.ExcelAddIn.Models;

namespace SeriesEngine.ExcelAddIn.Presenters
{
    public class PeriodVariableEditorPresenter : Presenter<IPeriodVariableView>,
        ICommand<EditPeriodVariableCommandArg>
    {
        private readonly IObjectProvider _objectProvider;

        public PeriodVariableEditorPresenter(IObjectProvider objectProvider, IPeriodVariableView view, IApplicationController controller) : base(view, controller)
        {
            _objectProvider = objectProvider;
            View.EditVariableCompleted += (s, e) =>
            {
                _objectProvider.UpdatePeriodVaraible(View.ValuesCollection);                
            };
        }

        void ICommand<EditPeriodVariableCommandArg>.Execute(EditPeriodVariableCommandArg commandData)
        {
            var variablesValues = _objectProvider.GetSelectedPeriodVaraible(commandData.CurrentSelection, commandData.Solution);
            View.ValuesCollection = variablesValues;
            View.ShowIt();
        }
    }
}
