using SeriesEngine.App;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IObjectProvider
    {
        MyObject GetSelectedObject(CurrentSelection selection);
        void UpdateObject(MyObject objectToUpdate);
    }
}
