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
                NodeName = "Кемеровская область"
            };
            var consumer1 = new NetworkTreeNode
            {
                NodeName = "Шахта Юбилейная",
                Parent = region1
            };
            var contract1 = new NetworkTreeNode
            {
                NodeName = "Договор ЭН",
                Parent = consumer1
            };

            yield return new NetworkTree
            {
                Name = "Основные объекты",
                Nodes =
                {
                    region1,
                    consumer1,
                    contract1
                }
            };    
        }
    }
}
