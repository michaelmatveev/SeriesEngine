using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.msk1;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataBaseNetworkProvider : INetworksProvider
    {
        //public ICollection<NetworkTree> GetNetworks(int solutionId)
        //{
        //    using (var context = new Model1())
        //    {
        //        var nets = context
        //            .Networks
        //            .OfType<MainHierarchyNetwork>()
        //            .Include("Solution")
        //            .Include("Nodes")
        //            .Include("Nodes.Region")
        //            .Include("Nodes.Consumer")
        //            .Include("Nodes.Contract")
        //            .Include("Nodes.ConsumerObject")
        //            .Include("Nodes.Point")
        //            .Include("Nodes.ElectricMeter")
        //            .Where(n => n.SolutionId == solutionId)
        //            .ToList();
        //        return nets
        //            .Select(n => new NetworkTree(n))
        //            .ToList();
        //    }
        //}

        public NetworkTree GetNetworkById(int networkId)
        {
            using (var context = new Model1())
            {
                var network = context.Networks.Find(networkId);
                var query = context.Entry(network).Collection("Nodes").Query();
                foreach (var v in network.HierarchyModel.ReferencedObjects)
                {
                    query = query.Include(v.Name);
                }

                //var net = context
                //    .Networks
                //    .OfType<MainHierarchyNetwork>()
                //    .Include("Solution")
                //    .Include("Nodes")
                //    .Include("Nodes.Region")
                //    .Include("Nodes.Consumer")
                //    .Include("Nodes.Contract")
                //    .Include("Nodes.ConsumerObject")
                //    .Include("Nodes.Point")
                //    .Include("Nodes.ElectricMeter")
                //    .Single(n => n.Id == networkId);
                query.Load();
                return new NetworkTree(network);
            }
        }

        public NetworkTree GetNetwork(int solutionId, string name, IEnumerable<DataBlock> variables = null, Period period = null)
        {
            using (var context = new Model1())
            {
                var solution = context
                    .Solutions
                    .Include(s => s.Networks)
                    .SingleOrDefault(s => s.Id == solutionId);

                var network = solution.Networks.First(n => n.Name == name);
                var query = context.Entry(network).Collection("Nodes").Query();

                if (variables != null)
                {
                    foreach (var v in variables.OfType<VariableDataBlock>()
                        .Where(b => b.VariableMetamodel.IsPeriodic))
                    {
                        var obj = v.RefObject;
                        var vrb = v.VariableMetamodel.Name;
                        query = query.Include($"{obj}.{obj}_{vrb}s");
                    }

                    foreach (var v in variables.OfType<NodeDataBlock>()
                        .Where(n => n.NodeType == NodeType.UniqueName))
                    {
                        query = query.Include(v.RefObject);
                    }
                }

                query.Load();
                return new NetworkTree(network);
            }
        }

    }
}
