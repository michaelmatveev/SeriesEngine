using SeriesEngine.Core.DataAccess;
using System.Collections.Generic;

namespace SeriesEngine.Core.DataAccess
{
    public class EditPeriodVariables
    {
        public int NetworkId { get; set; }
        public Variable VariableMetamodel { get; set; }
        public NamedObject Object { get; set; }
        public List<PeriodVariable> ValuesForPeriod { get; private set; } = new List<PeriodVariable>();

        public EditPeriodVariables()
        {
        }
    }
}
