using SeriesEngine.Core.DataAccess;
using System;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IPeriodVariableView : IView
    {
        EditPeriodVariables ValuesCollection { get; set; }
        event EventHandler EditVariableCompleted;
        void ShowIt(Period selectedPeriod);
    }
}
