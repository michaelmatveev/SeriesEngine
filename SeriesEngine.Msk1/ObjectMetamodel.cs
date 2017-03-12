using SeriesEngine.Core;
using System.Collections.Generic;

namespace SeriesEngine.Msk1
{
    public class Variable
    {
        public string Name { get; set; }
        public bool IsPeriodic { get; set; }
        public bool IsVersioned { get; set; }
        public TimeInterval PeriodInterval { get; set; }
    }

    public class ObjectMetamodel
    {
        public string Name { get; set; }
        public List<Variable> Variables { get; set; }
    }
}
