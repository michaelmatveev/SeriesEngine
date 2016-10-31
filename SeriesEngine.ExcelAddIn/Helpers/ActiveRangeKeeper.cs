using Microsoft.Office.Interop.Excel;
using System;

namespace SeriesEngine.ExcelAddIn.Helpers
{
    public class ActiveRangeKeeper : IDisposable
    {
        private Worksheet _activeSheet;
        private Range _activeRange;

        public ActiveRangeKeeper(Microsoft.Office.Tools.Excel.Workbook workbook)
        {
            _activeSheet = (Worksheet)workbook.ActiveSheet;
            _activeRange = workbook.Application.ActiveCell;
            workbook.Application.ScreenUpdating = false;
        }

        public void Dispose()
        {
            _activeSheet.Activate();
            _activeRange.Select();
            _activeSheet.Application.ScreenUpdating = true;
        }
    }
}
