using SeriesEngine.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace SeriesEngine.Core.DataAccess
{
    public abstract class NamedObject : IStateObject
    {
        [NotMapped]
        public ObjectMetamodel ObjectModel { get; private set; }

        public NamedObject(ObjectMetamodel objectModel)
        {
            ObjectModel = objectModel;
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        [MaxLength]
        public string Name { get; set; }

        public int? AuthorId { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ConcurrencyStamp { get; set; }

        public int? Tag { get; set; }

        public virtual User User { get; set; }

        public int SolutionId { get; set; }

        public virtual Solution Solution { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ObjectCreationTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTime { get; set; }

        public abstract string GetName();
        public abstract void SetName(string newName);

        [NotMapped]
        public ObjectState State { get; set; }

        public object GetVariableValue(Variable variableModel, Period requestedPeriod, int shift)
        {
            var thisType = GetType();
            PropertyInfo property;
            var period = requestedPeriod.Shift(variableModel.PeriodInterval, shift);
            if (variableModel.PeriodInterval == TimeInterval.Indefinite )
            {
                property = thisType.GetProperty($"{ObjectModel.Name}_{variableModel.Name}s");
                var collection = property.GetValue(this, null) as IEnumerable<PeriodVariable>;
                return collection
                    .Where(v => v.Date < period.Till)
                    .OrderBy(v => v.Date)
                    .ThenBy(v => v.Id)
                    .LastOrDefault()
                    ?.Value;
            }

            if (variableModel.IsFixedPeriod && variableModel.IsVersioned)
            {
                property = thisType.GetProperty($"{ObjectModel.Name}_{variableModel.Name}s");
                var collection = property.GetValue(this, null) as IEnumerable<PeriodVariable>;
                return collection
                    .Where(v => v.Date >= period.From && v.Date < period.Till)
                    .OrderBy(v => v.Date)
                    .ThenBy(v => v.Id)
                    .LastOrDefault()
                    ?.Value;
            }

            if (!variableModel.IsFixedPeriod && variableModel.IsVersioned)
            {
                property = thisType.GetProperty($"{ObjectModel.Name}_{variableModel.Name}s");
                var collection = property.GetValue(this, null) as IEnumerable<VersionedVariable>;
                return collection
                    .OrderBy(v => v.CreationTime)
                    .LastOrDefault()
                    ?.Value;
            }

            property = thisType.GetProperty(variableModel.Name);
            return property.GetValue(this, null);
        }

        public IEnumerable<PeriodVariable> GetPeriodVariable(Variable variableModel)
        {
            if (variableModel.PeriodInterval == TimeInterval.None)
            {
                throw new ArgumentException(nameof(variableModel));
            }

            var thisType = GetType();
            var property = thisType.GetProperty($"{ObjectModel.Name}_{variableModel.Name}s");
            var collection = property.GetValue(this, null) as IEnumerable<PeriodVariable>;
            return collection.OrderBy(pv => pv.Date);
        }

        public bool SetVariableValue(string variableName, string value)
        {
            var thisType = GetType();
            var property = thisType.GetProperty(variableName);
            var newValue = ObjectModel
                .Variables
                .Single(v => v.Name == variableName)
                .Parse(value);

            if (property == null || Object.Equals(property.GetValue(this), newValue))
            {
                return false;
            }

            property.SetValue(this, newValue);
            return true;
        }

    }
}