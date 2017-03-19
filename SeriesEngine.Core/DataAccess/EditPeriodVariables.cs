using System.Collections.Generic;

namespace SeriesEngine.Core.DataAccess
{
    public class EditPeriodVariables
    {
        public Variable VariableMetamodel { get; set; }
        public string ObjectName { get; set; }
        public List<EditorPeriodVariable> ValuesForPeriod { get; private set; } = new List<EditorPeriodVariable>();

        public EditPeriodVariables()
        {
        }
    }
}
