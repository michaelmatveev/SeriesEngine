using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.Core;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataBaseNetworkProvider : INetworksProvider
    {
        public NetworkTree GetNetworkById(int networkId)
        {
            using (var context = ModelsDescription.GetModel(""))
            {
                var network = context.Networks.Find(networkId);
                var query = context.Entry(network).Collection("Nodes").Query();
                foreach (var v in network.HierarchyModel.ReferencedObjects)
                {
                    query = query.Include(v.Name);
                }
                query.Load();
                return new NetworkTree(network, false);
            }
        }

        public NetworkTree GetNetwork(Solution solution, string name, IEnumerable<DataBlock> variables = null, Period period = null)
        {
            using (var context = ModelsDescription.GetModel(solution.ModelName))
            {
                context.Solutions.Attach(solution);
                context.Entry(solution).Collection(s => s.Networks).Load();
                //var solution = context
                //    .Solutions
                //    .Include(s => s.Networks)
                //    .SingleOrDefault(s => s.Id == solutionId);

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

                var nodeObjects = variables
                    .OfType<NodeDataBlock>()
                    .Where(n => n.NodeType == NodeType.UniqueName)
                    .Select(v => v.RefObject);

                var fullHierarchy = network.HierarchyModel
                    .ReferencedObjects
                    .Select(o => o.Name)
                    .Except(nodeObjects).Count() == 0;
                
                return new NetworkTree(network, fullHierarchy);
            }
        }

    }
}
