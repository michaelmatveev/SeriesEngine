using SeriesEngine.App;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IObjectProvider
    {
        MyObject GetSelectedObject(CurrentSelection selection, Solution solution);
        void UpdateObject(MyObject objectToUpdate);
        void DeleteObject(CurrentSelection selection, Solution solution);
    }
}
