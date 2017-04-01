using Microsoft.Office.Interop.Excel;
using SeriesEngine.App;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class SelectionProvider : ISelectionProvider
    {
        public CurrentSelection GetSelection()
        {
            var range = (Range)Globals.ThisAddIn.Application.Selection;
            return new ExcelCurrentSelection(range);
        }
    }

    public class ExcelCurrentSelection : CurrentSelection
    {
        private readonly Range _range;

        public ExcelCurrentSelection(Range range)
        {
            _range = range;
        }

        public string Cell => _range.AddressLocal.Replace("$", string.Empty);
        public string Name => _range.AddressLocal.Replace("$", string.Empty);        
        public string Sheet => _range.Parent.Name;
        public int Row => _range.Row;
        public int Column => _range.Column;
        public override string Value {
            get
            {
                return _range.Value2;
            }
            set
            {
                _range.Value2 = value;
            }
        }
    }
}
