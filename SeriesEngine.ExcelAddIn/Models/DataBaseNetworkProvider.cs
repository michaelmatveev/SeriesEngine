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
                        .Where(b => b.VariableMetamodel.IsPeriodic || b.VariableMetamodel.IsVersioned))
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
