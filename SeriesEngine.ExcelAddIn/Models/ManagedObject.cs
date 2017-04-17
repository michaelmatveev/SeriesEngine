using SeriesEngine.Core;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ManagedObject
    {
        public ObjectMetamodel ObjectModel { get; set; }
        public string Name { get; set; }

        public Dictionary<string, object> _variables = new Dictionary<string, object>();
        public object this[string index]
        {
            get
            {
                object result;
                if(_variables.TryGetValue(index, out result))
                {
                    return result;
                }
                throw new InvalidOperationException($"Variable {index} not found");
            }
            set
            {
                _variables[index] = value;
            }
        }
    }
}
