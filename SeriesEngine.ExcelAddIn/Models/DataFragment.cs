using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public abstract class DataFragment : BaseFragment
    {
        public Period CustomPeriod { get; set; }

        public string Sheet { get; set; }
        public string Cell { get; set; }

        public DataFragment(BaseFragment parent, Period defaultPeriod)
        {
            Parent = parent;
            CustomPeriod = defaultPeriod;
        }

    }
}
