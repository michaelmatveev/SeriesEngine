﻿using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.msk1;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class MockNetworkProvider : INetworksProvider
    {
        public static Network mainTree;

        static MockNetworkProvider()
        {
            var region1 = new MainHierarchyNode
            {
                Region = new Region()
                {
                    Name = "Пензенская область"
                }
            };
            var consumer1 = new MainHierarchyNode
            {
                Parent = region1,
                Consumer = new Consumer
                {
                    Name = "OOO \"МагнитЭнерго\""
                }
            };
            var contract1 = new MainHierarchyNode
            {
                Parent = consumer1,
                ValidFrom = new DateTime(2016, 01, 01),
                ValidTill = new DateTime(2016, 06, 05),
                Contract = new Contract
                {
                    Name = "1001014-ЭН"
                }
            };
            contract1.Contract.ContractType = "КП";

            var consumerObject1 = new MainHierarchyNode
            {
                Parent = contract1,
                ConsumerObject = new ConsumerObject
                {
                    Name = "ММ \"Влад\"; г. Пенза пр-т. Строителей, 24а"
                }
            };
            //var point1 = new MainHierarchyNode
            //{
            //    Parent = consumerObject1,
            //    Point = new Point
            //    {
            //        Name = "ТП-530"
            //    }
            //};
            //point1.Point.VoltageLevel = "СН-2";

            //var point2 = new MainHierarchyNode
            //{
            //    Parent = consumerObject1,
            //    Point = new Point
            //    {
            //        Name = "ТП-531"
            //    }
            //};
            //point2.Point.VoltageLevel = "СН-2";

            var consumerObject2 = new MainHierarchyNode
            {
                Parent = contract1,
                ConsumerObject = new ConsumerObject
                {
                    Name = "ММ \"Арбеково\" г.Пенза пр. Строителей, 63"
                }
            };

            //var point3 = new MainHierarchyNode
            //{
            //    Parent = consumerObject2,
            //    Point = new Point
            //    {
            //        Name = "ТП-796"
            //    }
            //};
            //point3.Point.VoltageLevel = "СН-2";

            mainTree = new MainHierarchyNetwork()
            {
                Name = "Основные объекты",
            };

            mainTree.MyNodes.Add(region1);
            mainTree.MyNodes.Add(consumer1);
            mainTree.MyNodes.Add(contract1);
            mainTree.MyNodes.Add(consumerObject1);
            //mainTree.MyNodes.Add(point1);
            //mainTree.MyNodes.Add(point2);
            mainTree.MyNodes.Add(consumerObject2);
            //mainTree.MyNodes.Add(point3);
        }

        public ICollection<NetworkTree> GetNetworks(int solutionId)
        {
            //var result = new NetworkTree
            //{
            //    Name = mainTree.Name
            //};

            //if(string.IsNullOrEmpty(filter))
            //{
            //    result.Nodes.AddRange(mainTree.Nodes);
            //}

            //return new[] { result };

            return new[] { new NetworkTree(mainTree, true) };
        }

        public NetworkTree GetNetwork(Solution solution, string name, IEnumerable<DataBlock> variables, Period period)
        {
            return new NetworkTree(mainTree, true);
        }


        public NetworkTree GetNetworkById(string modelName, int networkId)
        {
            return new NetworkTree(mainTree, true);
        }

        public NetworkTree GetNetwork(Solution solution, CollectionDataBlock collectionDatablock)
        {
            throw new NotImplementedException();
        }
    }
}
