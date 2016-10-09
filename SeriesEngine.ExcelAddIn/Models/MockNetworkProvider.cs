using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Name = "Кемеровская область",
                    ObjectModel = MockModelProvider.Region
                }
            };
            var consumer1 = new NetworkTreeNode
            {
                Parent = region1,
                LinkedObject = new ManagedObject
                {
                    Name = "Шахта \"Юбилейная\"",
                    ObjectModel = MockModelProvider.Consumer
                }
            };
            var contract1 = new NetworkTreeNode
            {
                Parent = consumer1,
                LinkedObject = new ManagedObject
                {
                    Name = "0670413-ЭН",
                    ObjectModel = MockModelProvider.Contract
                }
            };
            var point1 = new NetworkTreeNode
            {
                Parent = contract1,
                LinkedObject = new ManagedObject
                {
                    Name = "ПС \"Сев - Байд\" 110/6 кВт Ф 6-14",
                    ObjectModel = MockModelProvider.Point
                }
            };
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

            mainTree = new NetworkTree
            {
                Name = "Основные объекты",
            };

            mainTree.Nodes.AddRange(new[]
            {
                region1, consumer1, contract1, point1, device1, device2
            });
        }

        public IEnumerable<Network> GetNetworks(string filter)
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

            yield return result;
        }
    }
}
