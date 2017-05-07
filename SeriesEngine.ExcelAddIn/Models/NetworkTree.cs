using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.msk1;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class NetworkTree
    {
        public const string RootName = "DataImportExport";
        private readonly Network _network;

        public NetworkTree(Network network)
        {
            _network = network;
        }

        public string Name => _network.Name;
        public int Id => _network.Id;
        public string SolutionName => _network.Solution.Name;

        public XDocument ConvertToXml(IEnumerable<DataBlock> queryParamers, Period defaultPeriod)
        {
            var data = new XDocument();
            var rootElement = new XElement(RootName);
            var tree = _network
                .MyNodes
                .Where(n => IsNodeInPeriod(n, defaultPeriod, false) && n.LinkedObject != null) // мы грузим все node для Network, но некторые из них не ссылаются на LinkedObject потому что соотвествующие объекты не были запрошены
                .GenerateTree(n => n.NodeName, n => n.MyParent?.NodeName);

            rootElement.Add(GetSubElements(tree, queryParamers));
            data.Add(rootElement);

            return data;
        }

        public NetworkTreeUpdater GetUpdater(DateTime defaultPeriod)
        {
            return new NetworkTreeUpdater(_network, defaultPeriod);
        }     

        private static bool IsNodeInPeriod(NetworkTreeNode node, Period period, bool whenNodePeriodIsIncorrectResult)
        {
            if (!(node.ValidFrom.HasValue || node.ValidTill.HasValue))
            {
                return true;
            }

            if (node.ValidFrom.HasValue && !node.ValidTill.HasValue)
            {
                return period.Till >= node.ValidFrom.Value;
            }

            if (node.ValidTill.HasValue && !node.ValidFrom.HasValue)
            {
                return period.From < node.ValidTill.Value;
            }

            if (node.ValidFrom.HasValue && node.ValidTill.HasValue)
            {
                return period.Intersect(node.ValidFrom.Value, node.ValidTill.Value);
            }

            return whenNodePeriodIsIncorrectResult;
        }

        public NamedObject FindObject(string objectTypeName, string name)
        {
            return _network
                .MyNodes
                .Where(n => n.LinkedObject?.ObjectModel.Name == objectTypeName)
                .SingleOrDefault(n => n.NodeName == name)
                ?.LinkedObject;
        }

        //TODO dublicate method in NetworkTreeUpdater
        public void Update(IEnumerable<IStateObject> valuesForPeriod)
        {
            try
            {
                using (var context = new Model1())
                {
                    //context.Configuration.AutoDetectChangesEnabled = false;
                    //context.Networks.Attach(_network);
                    foreach (var v in valuesForPeriod)
                    {
                        if (v.State == ObjectState.Added)
                        {
                            context.Set(v.GetType()).Add(v);
                        }
                        else
                        {
                            context.Set(v.GetType()).Attach(v);
                        }
                    }
                    context.FixState();
                    context.Database.Log = x => System.Diagnostics.Debug.WriteLine(x);
                    context.SaveChanges();
                }
            }
            catch(DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors.SelectMany(s => s.ValidationErrors);
                var message = string.Join(", ", errors.Select(e => e.ErrorMessage));
                throw new InvalidOperationException(message);
            }
        }

        public void RenameObjectLinkedWithNode(int nodeId, string newName)
        {
            using (var context = new Model1())
            {
                context.Database.Log = (s) => Debug.WriteLine(s);
                var node = _network.MyNodes.FirstOrDefault(n => n.Id == nodeId);
                //context.Entry(node).State = System.Data.Entity.EntityState.Unchanged;
                context.Entry(node.LinkedObject).State = System.Data.Entity.EntityState.Unchanged;
                node.LinkedObject.SetName(newName);

                context.SaveChanges();
            }
        }

        public void DeleteObjectLinkedWithNode(int nodeId)
        {
            using (var context = new Model1())
            {
                context.Database.Log = (s) => Debug.WriteLine(s);
                var node = _network.MyNodes.Single(n => n.Id == nodeId);
                context.Entry(node.LinkedObject).State = System.Data.Entity.EntityState.Deleted;

                context.SaveChanges();
            }
        }

        private IEnumerable<XElement> GetSubElements(IEnumerable<TreeItem<NetworkTreeNode>> currentItems, IEnumerable<DataBlock> queryParamers)
        {
            var result = new List<XElement>();
            foreach (var groupOfSameObjects in currentItems.GroupBy(c => c.Item.LinkedObject.ObjectModel.Name))
            {
                foreach (var node in groupOfSameObjects)
                {
                    var newElement = new XElement(groupOfSameObjects.Key);
                    foreach (var qp in queryParamers.Where(q => q.RefObject == groupOfSameObjects.Key))
                    {
                        ProcessObjectElement(newElement, node.Item, qp);
                    }
                    newElement.Add(GetSubElements(node.Children, queryParamers));
                    result.Add(newElement);
                }
            }
            return result;
        }

        private static void ProcessObjectElement(XElement newElement, NetworkTreeNode node, DataBlock qp)
        {
            if (qp is NodeDataBlock)
            {
                var nsf = (NodeDataBlock)qp;
                switch (nsf.NodeType)
                {
                    case NodeType.UniqueName:
                        newElement.Add(new XAttribute("UniqueName", node.NodeName), new XAttribute("NodeId", node.Id));
                        break;
                    case NodeType.Since:
                        if (node.ValidFrom.HasValue)
                        {
                            newElement.Add(new XAttribute("Since", node.ValidFrom));
                        }
                        break;
                    case NodeType.Till:
                        if (node.ValidTill.HasValue)
                        {
                            newElement.Add(new XAttribute("Till", node.ValidTill));
                        }
                        break;
                    default:
                        throw new NotSupportedException("this operation is not supported");
                }
            }
            else if (qp is VariableDataBlock) // переменные только для данного объекта
            {
                var vsf = (VariableDataBlock)qp;
                var varModel = vsf.VariableMetamodel;
                newElement.Add(new XElement(varModel.Name, node.LinkedObject.GetVariableValue(varModel, qp.VariablePeriod)));
            }
        }

    }
}