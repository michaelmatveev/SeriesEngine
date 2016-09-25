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

        public HashSet<Fragment> _fragments = new HashSet<Fragment>
        {
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 1",
                Sheet = "Лист1",
                Cell = "B11",
                ObjectMetamodel = MockModelProvider.Point,
                VariableMetamodel = MockModelProvider.Point.Variables.First(v => v.Name == "Наименование"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 2",
                Sheet = "Лист1",
                Cell = "C11",
                ObjectMetamodel = MockModelProvider.Point,
                VariableMetamodel = MockModelProvider.Point.Variables.First(v => v.Name == "Уровень напряжения"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 3",
                Sheet = "Лист1",
                Cell = "D11",
                ObjectMetamodel = MockModelProvider.Point,
                VariableMetamodel = MockModelProvider.Point.Variables.First(v => v.Name == "Максимальная мощность"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 4",
                Sheet = "Лист1",
                Cell = "E11",
                ObjectMetamodel = MockModelProvider.Point,
                VariableMetamodel = MockModelProvider.Point.Variables.First(v => v.Name == "Ценовая категория"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 5",
                Sheet = "Лист1",
                Cell = "F11",
                ObjectMetamodel = MockModelProvider.Device,
                VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Тип прибора учета"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 6",
                Sheet = "Лист1",
                Cell = "G11",
                ObjectMetamodel = MockModelProvider.Device,
                VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Номер счетчика"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 7",
                Sheet = "Лист1",
                Cell = "H11",
                ObjectMetamodel = MockModelProvider.Device,
                VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Направление перетока"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 8",
                Sheet = "Лист1",
                Cell = "I11",
                ObjectMetamodel = MockModelProvider.Device,
                VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Показание счетчика"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 9",
                Sheet = "Лист1",
                Cell = "J11",
                ObjectMetamodel = MockModelProvider.Device,
                VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Показание счетчика"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 10",
                Sheet = "Лист1",
                Cell = "L11",
                ObjectMetamodel = MockModelProvider.Device,
                VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Коэффициент трансформации"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 11",
                Sheet = "Лист1",
                Cell = "P11",
                ObjectMetamodel = MockModelProvider.Device,
                VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Потери"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            },
            new Fragment(SourceCollection, DefaultPeriod)
            {
                Name = "Фрагмент 12",
                Sheet = "Лист1",
                Cell = "Q11",
                ObjectMetamodel = MockModelProvider.Device,
                VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "ОДН"),
                Interval = TimeInterval.Month,
                IntervalsByRows = false,
                UseCommonPeriod = true,
                ShowIntervals = false
            }
        };

        public IEnumerable<Fragment> GetFragments()
        {
            return _fragments;
        }

        public Fragment CreateFragment(NamedCollection source)
        {
            var mock = new MockModelProvider();
            return new Fragment(SourceCollection, DefaultPeriod)
            {
                ObjectMetamodel = mock.GetObjectMetamodels().First(),
                VariableMetamodel = mock.GetObjectMetamodels().First().Variables.First()
            };
        }

        public Fragment CopyFragment(Fragment sourceFragment, NamedCollection sourceCollection)
        {
            return new Fragment(sourceCollection, sourceFragment.CustomPeriod)
            {
                Name = $"{sourceFragment.Name}_копия",
                Sheet = sourceFragment.Sheet,
                Cell = sourceFragment.Cell,
                ObjectMetamodel = sourceFragment.ObjectMetamodel,
                VariableMetamodel = sourceFragment.VariableMetamodel,
                Kind = sourceFragment.Kind,
                Interval = sourceFragment.Interval,
                IntervalsByRows = sourceFragment.IntervalsByRows,
                ShowIntervals = sourceFragment.ShowIntervals,
                UseCommonPeriod = sourceFragment.UseCommonPeriod,
                UseShift = sourceFragment.UseShift,
                Shift = sourceFragment.Shift,
                ShiftPeriod = sourceFragment.ShiftPeriod,
                CustomPeriod = sourceFragment.CustomPeriod
            };
        }

        public void AddFragment(Fragment fragment)
        {
            //if (!_fragments.Contains(fragment))
            //{
                _fragments.Add(fragment);
            //}
        }

        public void DeleteFragment(Fragment fragment)
        {
            _fragments.Remove(fragment);
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
