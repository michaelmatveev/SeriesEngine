using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.Msk1
{
    public abstract class NamedObject
    {
        public abstract string GetName();
        public ObjectMetamodel ObjectModel { get; set; }
        public object GetVariableValue(string variableName)
        {
            var thisType = this.GetType();
            var property = thisType.GetProperty(variableName);
            return property.GetValue(this, null);
        }
    }
}
