using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class MockFragmentsProvider : IFragmentsProvider
    {
        public IEnumerable<Fragment> GetFragments()
        {
            yield return new Fragment
            {
                Name = "Фрагмент 1",
                Cell = "A1"
            };
            yield return new Fragment
            {
                Name = "Фрагмент 2",
                Cell = "C1"
            };
            yield return new Fragment
            {
                Name = "Фрагмент 3",
                Cell = "D1"
            };
        }
    }
}
