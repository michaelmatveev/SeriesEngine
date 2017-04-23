﻿using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;

namespace SeriesEngine.Core
{
    public class Variable
    {
        public string Name { get; set; }
        public bool IsPeriodic { get; set; }
        public bool IsVersioned { get; set; }
        public TimeInterval PeriodInterval { get; set; }
        public Type EntityType { get; set; }

        internal object Parse(string value)
        {
            bool boolResult;
            if(bool.TryParse(value, out boolResult))
            {
                return boolResult;
            }
            int intResult;
            //if(int.TryParse(value, out intResult))
            //{
            //    return intResult;
            //}
            return value.Trim();
        }
    }

    public class ObjectMetamodel
    {
        public string Name { get; set; }
        public Type ObjectType { get; set; }
        public IEnumerable<Variable> Variables { get; set; }
    }

    public class HierarchyMemamodel
    {
        public string Name { get; set; }
        public IEnumerable<ObjectMetamodel> ReferencedObjects { get; set; }
        public Type NodeType { get; set; }
    }

    public class MetaModel
    {
        public string Name { get; set; }
        public IEnumerable<ObjectMetamodel> ObjectModels { get; set; }
        public IEnumerable<HierarchyMemamodel> HierarchyModels { get; set; }
    }

}
