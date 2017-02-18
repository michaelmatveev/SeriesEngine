using Microsoft.Office.Interop.Excel;
using SeriesEngine.App;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class SelectionProvider : ISelectionProvider
    {
        public CurrentSelection GetSelection()
        {
            var range = (Range)Globals.ThisAddIn.Application.Selection;
            var sheet = range.Parent.Name;
            var cell = range.AddressLocal.Replace("$", string.Empty);
            return new CurrentSelection
            {
                Sheet = sheet,
                Cell = cell,
                Name = cell
            };
        }
    }
}
