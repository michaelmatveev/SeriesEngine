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
        public NetworkTree GetNetworkById(string modelName, int networkId)
        {
            using (var context = ModelsDescription.GetModel(modelName))
            {
                var network = context.Networks.Find(networkId);
                context.Entry(network).Reference(n => n.Solution).Load();
                var query = context.Entry(network).Collection("Nodes").Query();
                //query.MergeOption = MergeOption.OverwriteChanges;
                foreach (var v in network.HierarchyModel.ReferencedObjects)
                {
                    query = query.Include(v.Name);
                }
                query.Load();
                return new NetworkTree(network, false);
            }
        }

        public NetworkTree GetNetwork(Solution solution, string networkName, IEnumerable<DataBlock> variables = null, Period period = null)
        {
            using (var context = ModelsDescription.GetModel(solution.ModelName))
            {
                var sln = context.Solutions.Find(solution.Id);
                context.Entry(sln).Collection(s => s.Networks).Load();
                var network = sln.Networks.First(n => n.Name == networkName);
                var query = context.Entry(network).Collection("Nodes").Query();

                bool fullHierarchy = false;

                if (variables != null)
                {
                    foreach (var v in variables.OfType<VariableDataBlock>()
                        .Where(b => b.VariableMetamodel.PeriodInterval != TimeInterval.None || b.VariableMetamodel.IsVersioned))
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

                    var nodeObjects = variables
                        .OfType<NodeDataBlock>()
                        .Where(n => n.NodeType == NodeType.UniqueName)
                        .Select(v => v.RefObject);

                    fullHierarchy = network.HierarchyModel
                        .ReferencedObjects
                        .Select(o => o.Name)
                        .Except(nodeObjects).Count() == 0;
                }

                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                query.Load();
                return new NetworkTree(network, fullHierarchy);
            }
        }

    }
}
