using SeriesEngine.ExcelAddIn.Models.Fragments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IDataImporter
    {
        void ImportFromFragments(IEnumerable<SheetFragment> fragments, Period period);
    }
}
