using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class MockNetworkProvider : INetworksProvider
    {
        public static NetworkTree mainTree;

        static MockNetworkProvider()
        {
            var region1 = new MainHierarchy
            {
                Region = new Region
                {
                    Name = "Пензенская область",
                    ObjectModel = MockModelProvider.Region
                }
            };
            var consumer1 = new MainHierarchy
            {
                MainHierarchy2 = region1,
                Consumer = new Consumer
                {
                    Name = "OOO \"МагнитЭнерго\"",
                    ObjectModel = MockModelProvider.Customer
                }
            };
            var contract1 = new MainHierarchy
            {
                MainHierarchy2 = consumer1,
                ValidFrom = new DateTime(2016, 01, 01),
                ValidTill = new DateTime(2016, 06, 05),
                Contract = new Contract
                {
                    Name = "1001014-ЭН",
                    ObjectModel = MockModelProvider.Contract
                }
            };
            contract1.Contract.ContractType = "КП";

            var consumerObject1 = new MainHierarchy
            {
                MainHierarchy2 = contract1,
                ConsumerObject = new ConsumerObject
                {
                    Name = "ММ \"Влад\"; г. Пенза пр-т. Строителей, 24а",
                    ObjectModel = MockModelProvider.ConsumerObject
                }
            };
            var point1 = new MainHierarchy
            {
                MainHierarchy2 = consumerObject1,
                Point = new Point
                {
                    Name = "ТП-530",
                    ObjectModel = MockModelProvider.Point
                }
            };
            point1.Point.VoltageLevel = "СН-2";

            var point2 = new MainHierarchy
            {
                MainHierarchy2 = consumerObject1,
                Point = new Point
                {
                    Name = "ТП-531",
                    ObjectModel = MockModelProvider.Point
                }
            };
            point2.Point.VoltageLevel = "СН-2";

            var consumerObject2 = new MainHierarchy
            {
                MainHierarchy2 = contract1,
                ConsumerObject = new ConsumerObject
                {
                    Name = "ММ \"Арбеково\" г.Пенза пр. Строителей, 63",
                    ObjectModel = MockModelProvider.ConsumerObject
                }
            };

            var point3 = new MainHierarchy
            {
                MainHierarchy2 = consumerObject2,
                Point = new Point
                {
                    Name = "ТП-796",
                    ObjectModel = MockModelProvider.Point
                }
            };
            point3.Point.VoltageLevel = "СН-2";

            mainTree = new NetworkTree
            {
                Name = "Основные объекты",
            };

            mainTree.MainHierarchies.Add(region1);
            mainTree.MainHierarchies.Add(consumer1);
            mainTree.MainHierarchies.Add(contract1);
            mainTree.MainHierarchies.Add(consumerObject1);
            mainTree.MainHierarchies.Add(point1);
            mainTree.MainHierarchies.Add(point2);
            mainTree.MainHierarchies.Add(consumerObject2);
            mainTree.MainHierarchies.Add(point3);
        }

        public ICollection<Network> GetNetworks(string filter)
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

            return new[] { mainTree };
        }
    }
}
