using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace SeriesEngine.Core.DataAccess
{
    public abstract class NamedObject : IStateObject
    {
        public int Id { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ObjectCreationTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTime { get; set; }

        public abstract string GetName();
        public abstract void SetName(string newName);

        [NotMapped]
        public ObjectMetamodel ObjectModel { get; set; }

        [NotMapped]
        public ObjectState State { get; set; }

        public object GetVariableValue(Variable variableModel, Period requestedPeriod)
        {
            var thisType = GetType();
            PropertyInfo property;
            if (variableModel.IsPeriodic || variableModel.IsVersioned)
            {
                property = thisType.GetProperty($"{ObjectModel.Name}_{variableModel.Name}s");
                var collection = property.GetValue(this, null) as IEnumerable<PeriodVariable>;
                return collection
                    .OrderBy(v => v.Date)
                    .LastOrDefault(v => v.Date < requestedPeriod.Till)
                    ?.Value;
            }

            property = thisType.GetProperty(variableModel.Name);
            return property.GetValue(this, null);
        }

        public IEnumerable<PeriodVariable> GetPeriodVariable(Variable variableModel)
        {
            if(!variableModel.IsPeriodic)
            {
                throw new ArgumentException(nameof(variableModel));
            }

            var thisType = GetType();
            var property = thisType.GetProperty($"{ObjectModel.Name}_{variableModel.Name}s");
            var collection = property.GetValue(this, null) as IEnumerable<PeriodVariable>;
            return collection.OrderBy(pv => pv.Date);
        }

        public bool SetVariableValue(string variableName, object value)
        {
            try
            {
                var thisType = GetType();
                var property = thisType.GetProperty(variableName);
                if(property.GetValue(this) == value)
                {
                    return false;
                }
                property.SetValue(this, value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
