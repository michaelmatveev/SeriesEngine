using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace SeriesEngine.Core
{
    public class Variable
    {
        public readonly Func<string, object> _parser;

        public Variable()
        {
        }

        public Variable(Func<string, object> parser)
        {
            _parser = parser;
        }

        public string Name { get; set; }
        public bool IsVersioned { get; set; }
        public TimeInterval PeriodInterval { get; set; }
        public bool IsFixedPeriod
        {
            get
            {
                return PeriodInterval != TimeInterval.None && PeriodInterval != TimeInterval.Indefinite ;
            }
        }

        public Type EntityType { get; set; }
        public Type ValueType { get; set; }

        public string ObjectName { get; set; }

        public object Parse(string value)
        {
            return _parser(value);
        }

        public bool IsOptional { get; set; }
        public Action<BaseModelContext, DbDataReader> DataLoader { get; set; }
    }

    public class ObjectMetamodel
    {
        public string Name { get; set; }
        public string ModelName { get; set; }

        public Type ObjectType { get; set; }
        public IEnumerable<Variable> Variables { get; set; }
        public Action<BaseModelContext, DbDataReader> DataLoader { get; set; }
    }

    public class HierarchyMemamodel
    {
        public string Name { get; set; }
        public string ModelName { get; set; }
        public IEnumerable<string> SystemNetworks;
        public IEnumerable<ObjectMetamodel> ReferencedObjects { get; set; }
        public Type NodeType { get; set; }
        public Type HierarchyType { get; set; }
        public Action<BaseModelContext, DbDataReader> DataLoader { get; set; }
    }

    public class MetaModel
    {
        public string Name { get; set; }
        public IEnumerable<ObjectMetamodel> ObjectModels { get; set; }
        public IEnumerable<HierarchyMemamodel> HierarchyModels { get; set; }
    }

}
