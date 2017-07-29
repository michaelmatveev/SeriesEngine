using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Business.Trees
{
    public class VariableGroup
    {
        //public Dictionary<DateTime, IList<string>> _slices = new Dictionary<DateTime, IList<string>>();
         
        public string Caption { get; set; }
        public Variable Variable { get; set; }
        public IList<NamedObject> ObjectsToScan; 
        public IList<object> GetSlice(DateTime d)
        {
            var period = new Period
            {
                From = d,
                Till = d.AddMilliseconds(1)
            };
            var result = new List<object>();
            foreach(var o in ObjectsToScan)
            {
                o.GetVariableValue(Variable, period);
            }
            return result;
        }

    }
}
