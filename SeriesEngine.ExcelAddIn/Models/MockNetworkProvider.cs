using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class MockNetworkProvider : INetworksProvider
    {
        public static NetworkTree mainTree;

        static MockNetworkProvider()
        {
            var region1 = new NetworkTreeNode
            {
                LinkedObject = new ManagedObject
                {
                    Name = "Пензенская область",
                    ObjectModel = MockModelProvider.Region
                }
            };
            var consumer1 = new NetworkTreeNode
            {
                Parent = region1,
                LinkedObject = new ManagedObject
                {
                    Name = "OOO \"МагнитЭнерго\"",
                    ObjectModel = MockModelProvider.Customer
                }
            };
            var contract1 = new NetworkTreeNode
            {
                Parent = consumer1,
                LinkedObject = new ManagedObject
                {
                    Name = "1001014-ЭН",
                    ObjectModel = MockModelProvider.Contract
                }
            };
            contract1.LinkedObject["ContractType"] = "КП";
            var consumerObject1 = new NetworkTreeNode
            {
                Parent = contract1,
                LinkedObject = new ManagedObject
                {
                    Name = "ММ \"Влад\"; г. Пенза пр-т. Строителей, 24а",
                    ObjectModel = MockModelProvider.ConsumerObject
                }
            };
            var point1 = new NetworkTreeNode
            {
                Parent = consumerObject1,
                LinkedObject = new ManagedObject
                {
                    Name = "ТП-530",
                    ObjectModel = MockModelProvider.Point
                }
            };
            point1.LinkedObject["VoltageLevel"] = "СН-2";
            var device1 = new NetworkTreeNode
            {
                Parent = point1,
                LinkedObject = new ManagedObject
                {
                    Name = "35588369",
                    ObjectModel = MockModelProvider.Device
                }
            };
            var device2 = new NetworkTreeNode
            {
                Parent = point1,
                LinkedObject = new ManagedObject
                {
                    Name = "35588466",
                    ObjectModel = MockModelProvider.Device
                }
            };
            var point2 = new NetworkTreeNode
            {
                Parent = consumerObject1,
                LinkedObject = new ManagedObject
                {
                    Name = "ТП-531",
                    ObjectModel = MockModelProvider.Point
                }
            };
            point2.LinkedObject["VoltageLevel"] = "СН-2";
            var consumerObject2 = new NetworkTreeNode
            {
                Parent = contract1,
                LinkedObject = new ManagedObject
                {
                    Name = "ММ \"Арбеково\" г.Пенза пр. Строителей, 63",
                    ObjectModel = MockModelProvider.ConsumerObject
                }
            };
            var point3 = new NetworkTreeNode
            {
                Parent = consumerObject2,
                LinkedObject = new ManagedObject
                {
                    Name = "ТП-796",
                    ObjectModel = MockModelProvider.Point
                }
            };
            point3.LinkedObject["VoltageLevel"] = "СН-2";

            mainTree = new NetworkTree
            {
                Name = "Основные объекты",
            };

            mainTree.Nodes.AddRange(new[]
            {
                region1, consumer1, contract1, consumerObject1, point1, device1, device2, point2, consumerObject2, point3
            });
        }

        public ICollection<Network> GetNetworks(string filter)
        {
            var result = new NetworkTree
            {
                Name = mainTree.Name
            };

            if(string.IsNullOrEmpty(filter))
            {
                result.Nodes.AddRange(mainTree.Nodes);
            }

            //var nodes = new List<NetworkTreeNode>();
            //if(string.IsNullOrEmpty(filter) || filter.Contains(region1.NodeName))
            //{
            //    nt.Nodes.Add(region1);
            //}
            //if (string.IsNullOrEmpty(filter) || filter.Contains(consumer1.NodeName))
            //{
            //    nt.Nodes.Add(consumer1);
            //}
            //if (string.IsNullOrEmpty(filter) || filter.Contains(contract1.NodeName))
            //{
            //    nt.Nodes.Add(contract1);
            //}
            //if (string.IsNullOrEmpty(filter) || filter.Contains(point1.NodeName))
            //{
            //    nt.Nodes.Add(point1);
            //}
            //if (string.IsNullOrEmpty(filter) || filter.Contains(device1.NodeName))
            //{
            //    nt.Nodes.Add(device1);
            //}
            //if (string.IsNullOrEmpty(filter) || filter.Contains(device2.NodeName))
            //{
            //    nt.Nodes.Add(device2);
            //}

            return new[] { result };
        }
    }
}
