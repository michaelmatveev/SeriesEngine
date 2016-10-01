using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IFragmentsProvider
    {
        IEnumerable<BaseFragment> GetFragments(string filter);
        Fragment CreateFragment(CollectionFragment source);
        Fragment CopyFragment(Fragment sourceFragment, CollectionFragment sourceCollection);
        void AddFragment(Fragment fragment);
        void DeleteFragment(Fragment fragment);

        Period GetDefaultPeriod();
        void SetDefaultPeriod(Period p);
    }   
}
