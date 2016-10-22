using SeriesEngine.ExcelAddIn.Models.Fragments;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public interface IFragmentsProvider
    {
        IEnumerable<BaseFragment> GetFragments(string filter);
        DataFragment CreateFragment(CollectionFragment source);
        DataFragment CopyFragment(DataFragment sourceFragment, CollectionFragment sourceCollection);
        void AddFragment(DataFragment fragment);
        void DeleteFragment(DataFragment fragment);

        Period GetDefaultPeriod();
        void SetDefaultPeriod(Period p);
    }   
}
