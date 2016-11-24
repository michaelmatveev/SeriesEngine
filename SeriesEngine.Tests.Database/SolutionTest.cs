﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SeriesEngine.Core.DataAccess;
using SeriesEngine.Msk1;

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
                    Name = "solution1",
                    Description = "test description"
                };
                //context.Solutions.Add(solution);

                var region = new Region()
                {
                    Name = "Пензенская область"
                };

                //context.Regions.Add(region);

                var consumer = new Consumer()
                {
                    Name = "OOO \"МагнитЭнерго\""
                };

                var contract = new Contract()
                {
                    Name = "1001014 - ЭН",
                    ContractType = "КП"
                };

                var consumerObject1 = new ConsumerObject()
                {
                    Name = "ММ \"Влад\"; г.Пенза пр-т.Строителей, 24а",
                };

                var consumerObject2 = new ConsumerObject()
                {
                    Name = "ММ \"Арбеково\" г.Пенза пр.Строителей, 63"
                };

                var point1 = new Point()
                {
                    Name = "ТП - 530",
                    VoltageLevel = "СН - 2",
                    MaxPower = "10"
                };

                var point2 = new Point()
                {
                    Name = "ТП-531",
                    VoltageLevel = "СН - 2",
                    MaxPower = "20"
                };

                var point3 = new Point()
                {
                    Name = "ТП - 530",
                    VoltageLevel = "ТП-796",
                    MaxPower = "30"
                };

                var network = new Network()
                {
                    Name = "Основная коллекция объектов",
                    Solution = solution
                };

                var node1 = new MainHierarchyNode()
                {
                    Region = region,
                    Parent = null,
                    Network = network
                };

                var node2 = new MainHierarchyNode()
                {
                    Consumer = consumer,
                    Parent = node1,
                    Network = network
                };

                var node3 = new MainHierarchyNode()
                {
                    Contract = contract,
                    Parent = node2,
                    Network = network
                };

                var node4 = new MainHierarchyNode()
                {
                    ConsumerObject = consumerObject1,
                    Parent = node3,
                    Network = network
                };

                var node5 = new MainHierarchyNode()
                {
                    ConsumerObject = consumerObject2,
                    Parent = node3,
                    Network = network
                };

                var node6 = new MainHierarchyNode()
                {
                    Point = point1,
                    Parent = node4,
                    Network = network
                };

                var node7 = new MainHierarchyNode()
                {
                    Point = point2,
                    Parent = node4,
                    Network = network
                };

                var node8 = new MainHierarchyNode()
                {
                    Point = point3,
                    Parent = node5,
                    Network = network
                };

                network.Nodes.Add(node1);
                network.Nodes.Add(node2);
                network.Nodes.Add(node3);
                network.Nodes.Add(node4);
                network.Nodes.Add(node5);
                network.Nodes.Add(node6);
                network.Nodes.Add(node7);
                network.Nodes.Add(node8);

                context.Networks.Add(network);
                try
                {
                    context.SaveChanges();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
