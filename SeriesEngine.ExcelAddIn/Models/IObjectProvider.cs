using SeriesEngine.App;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IObjectProvider
    {
        EditorObject GetSelectedObject(CurrentSelection selection, Solution solution);
        EditPeriodVariables GetSelectedPeriodVaraible(CurrentSelection selection, Solution solution);
        void UpdatePeriodVaraible(EditPeriodVariables variables);
        void RenameObject(EditorObject objectToUpdate);
        void DeleteObject(EditorObject objectToDelete);
    }
}
