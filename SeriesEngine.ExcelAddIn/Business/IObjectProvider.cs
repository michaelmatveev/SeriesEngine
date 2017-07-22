using SeriesEngine.App;
using SeriesEngine.Core.DataAccess;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IObjectProvider
    {
        EditorObject GetSelectedObject(CurrentSelection selection, Solution solution);
        EditPeriodVariables GetSelectedPeriodVaraible(CurrentSelection selection, Solution solution);
        void RenameObject(string modelName, EditorObject objectToUpdate);
        void DeleteObject(string modelName, EditorObject objectToDelete);

        //void UpdatePeriodVaraible(EditPeriodVariables variables);
        void ChangeData(string modelName, int networkId, IEnumerable<IStateObject> objectsToChange);

    }
}
