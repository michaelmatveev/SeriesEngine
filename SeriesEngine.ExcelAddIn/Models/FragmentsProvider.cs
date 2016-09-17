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
                Cell = "A1",
                Interval = TimeInterval.Hour
            };
            yield return new Fragment
            {
                Name = "Фрагмент 2",
                Cell = "C1",
                Interval = TimeInterval.Day
            };
            yield return new Fragment
            {
                Name = "Фрагмент 3",
                Cell = "E1",
                Interval= TimeInterval.Month
            };
            yield return new Fragment
            {
                Name = "Фрагмент 4",
                Cell = "G1",
                Interval = TimeInterval.Year
            };
        }

        private Period _defaultPeriod = new Period
        {
            From = new DateTime(2016, 01, 01),
            Till = new DateTime(2016, 03, 01)
        };

        public Period GetDefaultPeriod()
        {
            return _defaultPeriod; 
        }

        public void SetDefaultPeriod(Period p)
        {
            _defaultPeriod = p;
        }
    }
}
