using FluentDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class MockFragmentsProvider : IFragmentsProvider
    {
        private static Period DefaultPeriod = new Period
        {
            From = DateTime.Now.FirstDayOfMonth(),
            Till = DateTime.Now.AddMonths(1).FirstDayOfMonth()
        };

        private static NamedCollection SourceCollection = new NamedCollection
        {
            Name = "Коллекция 1"
        };

        public List<Fragment> _fragments = new List<Fragment>
        {
            new Fragment
            {
                Name = "Фрагмент 1",
                Sheet = "Лист1",
                Cell = "A1",
                Interval = TimeInterval.Hour,
                SourceCollection = SourceCollection,
                IntervalsByRows = true,
                UseCommonPeriod = true,
                CustomPeriod = DefaultPeriod
            },
            new Fragment
            {
                Name = "Фрагмент 2",
                Sheet = "Лист1",
                Cell = "C1",
                Interval = TimeInterval.Day,
                SourceCollection = SourceCollection,
                IntervalsByRows = true,
                UseCommonPeriod = true,
                CustomPeriod = DefaultPeriod
            },
            new Fragment
            {
                Name = "Фрагмент 3",
                Sheet = "Лист1",
                Cell = "E1",
                Interval= TimeInterval.Month,
                SourceCollection = SourceCollection,
                IntervalsByRows = true,
                UseCommonPeriod = true,
                CustomPeriod = DefaultPeriod
            },
            new Fragment
            {
                Name = "Фрагмент 4",
                Sheet = "Лист1",
                Cell = "G1",
                Interval = TimeInterval.Year,
                SourceCollection = SourceCollection,
                IntervalsByRows = true,
                UseCommonPeriod = true,
                CustomPeriod = DefaultPeriod
            }
        };

        public IEnumerable<Fragment> GetFragments()
        {
            return _fragments;
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
