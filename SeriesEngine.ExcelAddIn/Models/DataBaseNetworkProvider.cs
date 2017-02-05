using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.Msk1;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataBaseNetworkProvider : INetworksProvider
    {
        public ICollection<NetworkTree> GetNetworks(int solutionId)
        {
            using (var context = new Model1())
            {
                var nets = context
                    .Networks
                    .Include("Solution")
                    .Include("Nodes")
                    .Include("Nodes.Region")
                    .Include("Nodes.Consumer")
                    .Include("Nodes.Contract")
                    .Include("Nodes.ConsumerObject")
                    .Include("Nodes.Point")
                    .Where(n => n.SolutionId == solutionId)
                    .ToList();
                return nets
                    .Select(n => new NetworkTree(n))
                    .ToList();
            }
        }
    }
}
