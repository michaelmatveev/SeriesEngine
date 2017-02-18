using SeriesEngine.App;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface ISelectionProvider
    {
        CurrentSelection GetSelection();
    }
}
