using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class MockNetworkProvider : INetworksProvider
    {

        public IEnumerable<Network> GetNetworks()
        {
            var region1 = new NetworkTreeNode
            {
                NodeName = "Кемеровская область",
                LinckedObject = new ManagedObject
                {
                    ObjectModel = MockModelProvider.Region
                }
            };
            var consumer1 = new NetworkTreeNode
            {
                NodeName = "Шахта \"Юбилейная\"",
                Parent = region1,
                LinckedObject = new ManagedObject
                {
                    ObjectModel = MockModelProvider.Consumer
                }
            };
            var contract1 = new NetworkTreeNode
            {
                NodeName = "0670413-ЭН",
                Parent = consumer1,
                LinckedObject = new ManagedObject
                {
                    ObjectModel = MockModelProvider.Contract
                }
            };
            var point1 = new NetworkTreeNode
            {
                NodeName = "ПС \"Сев - Байд\" 110/6 кВт Ф 6-14",
                Parent = contract1,
                LinckedObject = new ManagedObject
                {
                    ObjectModel = MockModelProvider.Point
                }
            };
            var device1 = new NetworkTreeNode
            {
                NodeName = "35588369",
                Parent = point1,
                LinckedObject = new ManagedObject
                {
                    ObjectModel = MockModelProvider.Device
                }
            };
            var device2 = new NetworkTreeNode
            {
                NodeName = "35588466",
                Parent = point1,
                LinckedObject = new ManagedObject
                {
                    ObjectModel = MockModelProvider.Device
                }
            };

            yield return new NetworkTree
            {
                Name = "Основные объекты",
                Nodes =
                {
                    region1,
                    consumer1,
                    contract1,
                    point1,
                    device1,
                    device2
                }
            };    
        }
    }
}
