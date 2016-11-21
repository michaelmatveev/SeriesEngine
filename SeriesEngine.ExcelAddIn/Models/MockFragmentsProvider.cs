﻿using FluentDateTime;
using SeriesEngine.ExcelAddIn.Models.Fragments;
using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class MockFragmentsProvider : IFragmentsProvider
    {
        private static Period DefaultPeriod = new Period
        {
            From = DateTime.Now.FirstDayOfMonth(),
            Till = DateTime.Now.AddMonths(1).FirstDayOfMonth()
        };

        private static CollectionFragment SourceCollection = new CollectionFragment
        {
            Name = "Потребители-Точки учета",
            SupportedModels = new []
            {                
                MockModelProvider.Contract,
                MockModelProvider.Point,
                MockModelProvider.Device
            }
        };

        //private static ObjectGridFragment ObjectGrid = new ObjectGridFragment(SourceCollection, DefaultPeriod)
        //{
        //    Name = "Объекты",
        //    Sheet = "Лист1",
        //    Cell = "A1",
        //};

        //private static CollectionFragment AllRegions = new CollectionFragment
        //{
        //    Name = MockModelProvider.Region.Name,
        //    Parent = SourceCollection,
        //    SupportedModels = new[]
        //    {
        //        MockModelProvider.Contract,
        //        MockModelProvider.Point,
        //        MockModelProvider.Device
        //    }
        //};

        //private static CollectionFragment AllConsumers = new CollectionFragment
        //{
        //    Name = MockModelProvider.Consumer.Name,
        //    Parent = AllRegions,
        //    SupportedModels = new[]
        //    {
        //        MockModelProvider.Contract,
        //        MockModelProvider.Point,
        //        MockModelProvider.Device
        //    }
        //};

        //private static CollectionFragment AllContracts = new CollectionFragment
        //{
        //    Name = MockModelProvider.Contract.Name,
        //    Parent = AllConsumers,
        //    SupportedModels = new[]
        //    {
        //        MockModelProvider.Contract,
        //        MockModelProvider.Point,
        //        MockModelProvider.Device
        //    }
        //};

        //private static CollectionFragment AllPoints = new CollectionFragment
        //{
        //    Name = MockModelProvider.Point.Name,
        //    Parent = AllContracts,
        //    SupportedModels = new[]
        //    {
        //        MockModelProvider.Contract,
        //        MockModelProvider.Point,
        //        MockModelProvider.Device
        //    }
        //};

        //private static CollectionFragment AllDevices = new CollectionFragment
        //{
        //    Name = MockModelProvider.Device.Name,
        //    Parent = AllPoints,
        //    SupportedModels = new[]
        //    {
        //        MockModelProvider.Contract,
        //        MockModelProvider.Point,
        //        MockModelProvider.Device
        //    }
        //};

        public HashSet<SheetFragment> _fragments = new HashSet<SheetFragment>
        {
            //new NodeFragment(null, DefaultPeriod)
            //{
            //    Name = "НазваниеРегиона",
            //    Sheet = "Лист1",
            //    Cell = "A4",
            //    NodeValue = ExportNodeValue.Name,
            //    Model = MockModelProvider.Region
            //}
        };

        //public HashSet<BaseFragment> _fragments = new HashSet<BaseFragment>
        //{
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 1",
        //        Sheet = "Лист1",
        //        Cell = "B11",
        //        ObjectMetamodel = MockModelProvider.Point,
        //        VariableMetamodel = MockModelProvider.Point.Variables.First(v => v.Name == "Наименование"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 2",
        //        Sheet = "Лист1",
        //        Cell = "C11",
        //        ObjectMetamodel = MockModelProvider.Point,
        //        VariableMetamodel = MockModelProvider.Point.Variables.First(v => v.Name == "Уровень напряжения"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 3",
        //        Sheet = "Лист1",
        //        Cell = "D11",
        //        ObjectMetamodel = MockModelProvider.Point,
        //        VariableMetamodel = MockModelProvider.Point.Variables.First(v => v.Name == "Максимальная мощность"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 4",
        //        Sheet = "Лист1",
        //        Cell = "E11",
        //        ObjectMetamodel = MockModelProvider.Point,
        //        VariableMetamodel = MockModelProvider.Point.Variables.First(v => v.Name == "Ценовая категория"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 5",
        //        Sheet = "Лист1",
        //        Cell = "F11",
        //        ObjectMetamodel = MockModelProvider.Device,
        //        VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Тип прибора учета"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 6",
        //        Sheet = "Лист1",
        //        Cell = "G11",
        //        ObjectMetamodel = MockModelProvider.Device,
        //        VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Номер счетчика"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 7",
        //        Sheet = "Лист1",
        //        Cell = "H11",
        //        ObjectMetamodel = MockModelProvider.Device,
        //        VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Направление перетока"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 8",
        //        Sheet = "Лист1",
        //        Cell = "I11",
        //        ObjectMetamodel = MockModelProvider.Device,
        //        VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Показание счетчика"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 9",
        //        Sheet = "Лист1",
        //        Cell = "J11",
        //        ObjectMetamodel = MockModelProvider.Device,
        //        VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Показание счетчика"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 10",
        //        Sheet = "Лист1",
        //        Cell = "L11",
        //        ObjectMetamodel = MockModelProvider.Device,
        //        VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Коэффициент трансформации"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 11",
        //        Sheet = "Лист1",
        //        Cell = "P11",
        //        ObjectMetamodel = MockModelProvider.Device,
        //        VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "Потери"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    },
        //    new Fragment(AllDevices, DefaultPeriod)
        //    {
        //        Name = "Фрагмент 12",
        //        Sheet = "Лист1",
        //        Cell = "Q11",
        //        ObjectMetamodel = MockModelProvider.Device,
        //        VariableMetamodel = MockModelProvider.Device.Variables.First(v => v.Name == "ОДН"),
        //        Interval = TimeInterval.Month,
        //        IntervalsByRows = false,
        //        UseCommonPeriod = true,
        //        ShowIntervals = false
        //    }
        //};

        public IEnumerable<BaseFragment> GetFragments(string filter)
        {
            MockNetworkProvider networkProvider = new MockNetworkProvider();
            CollectionFragment source = null;
            foreach(var network in networkProvider.GetNetworks(filter))
            {
                source = new CollectionFragment
                {
                    Name = network.Name
                };
                yield return source;
                yield return new ObjectGridFragment(source, DefaultPeriod)
                {
                    Name = "СтруктураОбъектов",
                    Sheet = "Лист1",
                    Cell = "A1",
                };

                //var supportedModels = new List<ObjectMetamodel>();
                //if (network is NetworkTree)
                //{
                //    var netTree = network as NetworkTree;
                //    var groupedObjects = netTree.Nodes.GroupBy((n) => n.LinkedObject.ObjectModel);
                    
                //    foreach(var g in groupedObjects)
                //    {
                //        supportedModels.Add(g.Key);
                //        var newFragment = new CollectionFragment
                //        {
                //            Name = $"{g.Key.Name} ({g.Count()})",
                //            Parent = source,
                //            SupportedModels = supportedModels.ToArray()
                //        };
                //        yield return newFragment;
                //        source = newFragment;
                //    }
                //}
            }

            // source - последний CollectionFragment, к котрому можно цеплять DataFragmet
            foreach (var f in _fragments)
            {
                f.Parent = source;
                yield return f;
            }

        }

        public DataFragment CreateFragment(CollectionFragment source)
        {
            var mock = new MockModelProvider();
            return new DataFragment(SourceCollection, DefaultPeriod)
            {
                ObjectMetamodel = mock.GetObjectMetamodels().First(),
                VariableMetamodel = mock.GetObjectMetamodels().First().Variables.First()
            };
        }

        public DataFragment CopyFragment(DataFragment sourceFragment, CollectionFragment sourceCollection)
        {
            return new DataFragment(sourceCollection, sourceFragment.CustomPeriod)
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

        public void AddFragment(DataFragment fragment)
        {
            //if (!_fragments.Contains(fragment))
            //{
                _fragments.Add(fragment);
            //}
        }

        public void DeleteFragment(DataFragment fragment)
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