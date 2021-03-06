﻿using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Business.Trees
{
    public class VariableGroup : TableGroup
    {       
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
                result.Add(o.GetVariableValue(Variable, period, 0));
            }
            return result;
        }
    }
}
