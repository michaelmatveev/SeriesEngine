﻿using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.msk1;

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
                    .OfType<MainHierarchyNetwork>()
                    .Include("Solution")
                    .Include("Nodes")
                    .Include("Nodes.Region")
                    .Include("Nodes.Consumer")
                    .Include("Nodes.Contract")
                    .Include("Nodes.ConsumerObject")
                    .Include("Nodes.Point")
                    .Include("Nodes.ElectricMeter")
                    .Where(n => n.SolutionId == solutionId)
                    .ToList();
                return nets
                    .Select(n => new NetworkTree(n))
                    .ToList();
            }
        }

        public NetworkTree GetNetworkById(int networkId)
        {
            using (var context = new Model1())
            {
                var net = context
                    .Networks
                    .OfType<MainHierarchyNetwork>()
                    .Include("Solution")
                    .Include("Nodes")
                    .Include("Nodes.Region")
                    .Include("Nodes.Consumer")
                    .Include("Nodes.Contract")
                    .Include("Nodes.ConsumerObject")
                    .Include("Nodes.Point")
                    .Include("Nodes.ElectricMeter")
                    .Single(n => n.Id == networkId);

                return new NetworkTree(net);
            }
        }

        public NetworkTree GetNetwork(int solutionId, string name, IEnumerable<VariableDataBlock> variables, Period period)
        {
            using (var context = new Model1())
            {
                var query = context.Networks.OfType<MainHierarchyNetwork>().AsQueryable();
                foreach (var v in variables)
                {
                    var obj = v.RefObject;
                    var vrb = v.VariableMetamodel.Name;
                    query = query.Include($"Nodes.{obj}.{obj}_{vrb}s");
                }

                var net = query
                    .Include("Solution")
                    .Include("Nodes")
                    .Include("Nodes.Region")
                    .Include("Nodes.Consumer")
                    .Include("Nodes.Contract")
                    .Include("Nodes.ConsumerObject")
                    .Include("Nodes.Point")
                    .Include("Nodes.ElectricMeter")                    
                    //.AsNoTracking()
                    .Single(n => n.SolutionId == solutionId && n.Name == name);

                return new NetworkTree(net);
            }

        }

    }
}
