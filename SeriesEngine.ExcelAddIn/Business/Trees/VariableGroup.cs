using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Business.Trees
{
    public class VariableGroup
    {
        public Dictionary<DateTime, IList<string>> _slices;
         
        public string Caption { get; set; }
        public IList<string> GetSlice(DateTime d)
        {
            return _slices[d];
        }

    }
}
