using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Views
{
    public interface IPeriodVariableView : IView
    {
        void ShowIt(EditPeriodVariables valuesColection);
    }
}
