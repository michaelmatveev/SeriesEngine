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

        public object GetVariableValue(Variable variableModel)
        {
            var thisType = GetType();
            PropertyInfo property;
            if (variableModel.IsPeriodic || variableModel.IsVersioned)
            {
                property = thisType.GetProperty($"{ObjectModel.Name}_{variableModel.Name}s");
                var collection = property.GetValue(this, null) as IEnumerable<PeriodVariable>;
                return collection.LastOrDefault()?.Value;
            }

            property = thisType.GetProperty(variableModel.Name);
            return property.GetValue(this, null);
        }

        public IEnumerable<PeriodVariable> GetPeriodVariable(Variable variableModel, Period selectedPeriod)
        {
            if(!variableModel.IsPeriodic)
            {
                throw new ArgumentException(nameof(variableModel));
            }

            var thisType = GetType();
            var property = thisType.GetProperty($"{ObjectModel.Name}_{variableModel.Name}s");
            var collection = property.GetValue(this, null) as IEnumerable<PeriodVariable>;
            return collection
                .Where(pv => selectedPeriod.Include(pv.Date))
                .OrderBy(pv => pv.Date);
        }

        public void SetVariableValue(string variableName, object value)
        {
            try
            {
                var thisType = GetType();
                var property = thisType.GetProperty(variableName);
                property.SetValue(this, value);
                this.State = ObjectState.Modified;
            }
            catch
            {

            }
        }
    }
}
