using SeriesEngine.Core.DataAccess;
using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.App.CommandArgs
{
    public class EditPeriodVariableCommandArg : BaseCommandArgs
    {
        public CurrentSelection CurrentSelection { get; set; }
        public IList<PeriodVariable> Variables;
    }
}
