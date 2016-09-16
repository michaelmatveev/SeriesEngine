using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IFragmentsProvider
    {
        IEnumerable<Fragment> GetFragments();
    }
}
