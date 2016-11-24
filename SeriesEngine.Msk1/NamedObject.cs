using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.Msk1
{
    public abstract class NamedObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ObjectCreationTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTime { get; set; }

        public abstract string GetName();
        [NotMapped]
        public ObjectMetamodel ObjectModel { get; set; }
        public object GetVariableValue(string variableName)
        {
            var thisType = this.GetType();
            var property = thisType.GetProperty(variableName);
            return property.GetValue(this, null);
        }
    }
}
