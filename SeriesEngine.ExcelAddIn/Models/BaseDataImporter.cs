using SeriesEngine.ExcelAddIn.Models.Fragments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public abstract class BaseDataImporter
    {
        protected void ImportFromFragments(IEnumerable<SheetFragment> fragments, Period period)
        {
            foreach (var f in fragments)
            {
                f.Import(this);  
            }            
        }

        public abstract void ImportFragment(ObjectGridFragment fragment);
    }
}
