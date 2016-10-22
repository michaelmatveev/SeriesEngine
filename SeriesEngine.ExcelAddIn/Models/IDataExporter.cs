using SeriesEngine.ExcelAddIn.Models.Fragments;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IDataExporter
    {
        void ExportFromFragments(IEnumerable<SheetFragment> fragments);
    }
}
