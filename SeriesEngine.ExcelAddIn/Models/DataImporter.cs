using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataImporter : IDataImporter
    {
        private Workbook _workbook;
        public DataImporter(Workbook workbook)
        {
            _workbook = workbook;
        }

        public void ImportFromFragments(IEnumerable<Fragment> fragments)
        {
            foreach(var f in fragments)
            {
                ImportFramgent(f);
            }
        }
        
        private void ImportFramgent(Fragment fragment)
        {
            Excel.Worksheet sheet = _workbook.Sheets["Лист1"];
            sheet.get_Range(fragment.Cell).Value = new Random().Next(100);
        }        
    }
}
