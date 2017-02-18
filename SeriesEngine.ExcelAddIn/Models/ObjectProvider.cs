using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using SeriesEngine.Core.DataAccess;
using System.Linq;
using SeriesEngine.App;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ObjectProvider : IObjectProvider
    {
        private readonly Workbook _workbook;

        public ObjectProvider(Workbook workbook)
        {
            _workbook = workbook;
        }

        public MyObject GetSelectedObject(CurrentSelection selection)
        {
            var sheet = _workbook.ActiveSheet as Worksheet;

            //var listObject = sheet
            //    .ListObjects
            //    .Cast<Excel.ListObject>()
            //    .SingleOrDefault(l => l.Name == selection.Name);

            //var value = listObject.ListColumns[selectedColumn].DataBodyRange.Offset[selectedRow, 0].Value2;
            return new MyObject
            {
                Name = "Test"
            };   
        }

        public void UpdateObject(MyObject objectToUpdate)
        {
   
        }
    }
}
