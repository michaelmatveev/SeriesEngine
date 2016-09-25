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
        Fragment CreateFragment(NamedCollection source);
        Fragment CopyFragment(Fragment sourceFragment, NamedCollection sourceCollection);
        void AddFragment(Fragment fragment);
        void DeleteFragment(Fragment fragment);

        Period GetDefaultPeriod();
        void SetDefaultPeriod(Period p);
    }   
}
