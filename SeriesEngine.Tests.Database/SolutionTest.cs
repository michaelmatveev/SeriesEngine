using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeriesEngine.msk1;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.Tests.Database
{
    [TestClass]
    public class SolutionTest
    {
        [TestMethod]
        public void CreateSolution()
        {
            using (var context = new Model1())
            {
                var solution = new Solution
                {
                    Name = "Тест 1",
                    Description = "Тестовое решение",
                    ModelName = "msk1"
                };

                var region = new Region()
                {
                    Solution = solution,
                    Name = "Пензенская область"
                };

                var consumer = new Consumer()
                {
                    Solution = solution,
                    Name = "OOO \"МагнитЭнерго\""
                };

                var contract = new Contract()
                {
                    Solution = solution,
                    Name = "1001014-ЭН",
                    ContractType = "КП"
                };

                var consumerObject1 = new ConsumerObject()
                {
                    Solution = solution,
                    Name = "ММ \"Влад\"; г.Пенза пр-т.Строителей, 24а",
                };

                var consumerObject2 = new ConsumerObject()
                {
                    Solution = solution,
                    Name = "ММ \"Арбеково\" г.Пенза пр.Строителей, 63"
                };

                var point1 = new Point()
                {
                    Solution = solution,
                    Name = "ТП-530",
                };

                point1.Point_VoltageLevels.Add(new Point_VoltageLevel()
                {
                    Date = new DateTime(2014, 01, 01),
                    //CreationTime = new DateTime(2014, 01, 01),
                    VoltageLevel = "СН-2"
                });
                point1.Point_MaxPowers.Add(new Point_MaxPower()
                {
                    Date = new DateTime(2014, 01, 01),
                    //CreationTime = new DateTime(2014, 01, 01),
                    MaxPower = 10
                });

                var point2 = new Point()
                {
                    Solution = solution,
                    Name = "ТП-531"
                };
                point2.Point_VoltageLevels.Add(new Point_VoltageLevel()
                {
                    Date = new DateTime(2014, 01, 01),
                    //CreationTime = new DateTime(2014, 01, 01),
                    VoltageLevel = "СН-2"
                });
                point2.Point_MaxPowers.Add(new Point_MaxPower()
                {
                    Date = new DateTime(2014, 01, 01),
                    //CreationTime = new DateTime(2014, 01, 01),
                    MaxPower = 20
                });

                var point3 = new Point()
                {
                    Solution = solution,
                    Name = "ТП-796",
                };
                point3.Point_VoltageLevels.Add(new Point_VoltageLevel()
                {
                    Date = new DateTime(2014, 01, 01),
                    //CreationTime = new DateTime(2014, 01, 01),
                    VoltageLevel = "СН-1"
                });
                point3.Point_MaxPowers.Add(new Point_MaxPower()
                {
                    Date = new DateTime(2014, 01, 01),
                    //CreationTime = new DateTime(2014, 01, 01),
                    MaxPower = 30
                });

                var network01 = new MainHierarchyNetwork()
                {
                    Name = "Регион - прибор учета",
                    Solution = solution
                };

                var network02 = new SupplierHierarchyNetwork()
                {
                    Name = "Поставщик - точка",
                    Solution = solution
                };

                var network03 = new CurcuitHierarchyNetwork()
                {
                    Name = "Сетевая организация - точка",
                    Solution = solution
                };

                var node1 = new MainHierarchyNode()
                {
                    Region = region,
                    Parent = null,
                    Network = network01
                };

                var node2 = new MainHierarchyNode()
                {
                    Consumer = consumer,
                    Parent = node1,
                    Network = network01
                };

                var node3 = new MainHierarchyNode()
                {
                    Contract = contract,
                    Parent = node2,
                    Network = network01
                };

                var node4 = new MainHierarchyNode()
                {
                    ConsumerObject = consumerObject1,
                    Parent = node3,
                    Network = network01
                };

                var node5 = new MainHierarchyNode()
                {
                    ConsumerObject = consumerObject2,
                    Parent = node3,
                    Network = network01
                };

                var node6 = new MainHierarchyNode()
                {
                    Point = point1,
                    Parent = node4,
                    Network = network01
                };

                var node7 = new MainHierarchyNode()
                {
                    Point = point2,
                    Parent = node4,
                    Network = network01
                };

                var node8 = new MainHierarchyNode()
                {
                    Point = point3,
                    Parent = node5,
                    Network = network01
                };

                network01.Nodes.Add(node1);
                network01.Nodes.Add(node2);
                network01.Nodes.Add(node3);
                network01.Nodes.Add(node4);
                network01.Nodes.Add(node5);
                network01.Nodes.Add(node6);
                network01.Nodes.Add(node7);
                network01.Nodes.Add(node8);

                context.Networks.Add(network01);
                context.Networks.Add(network02);
                context.Networks.Add(network03);

                var solution1 = new Solution
                {
                    Name = "Реальные данные 1",
                    Description = "Пример реальных данных",
                    ModelName = "msk1"
                };
                var network11 = new MainHierarchyNetwork()
                {
                    Name = "Регион - прибор учета",
                    Solution = solution1
                };
                var network12 = new SupplierHierarchyNetwork()
                {
                    Name = "Поставщик - точка",
                    Solution = solution1
                };
                var network13 = new CurcuitHierarchyNetwork()
                {
                    Name = "Сетевая организация - точка",
                    Solution = solution1
                };

                context.Networks.Add(network11);
                context.Networks.Add(network12);
                context.Networks.Add(network13);
                context.SaveChanges();
            }
        }
    }
}
