using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Business.Trees;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
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
        private readonly bool _fullHierarchy;

        public NetworkTree(Network network, bool fullHierarchy)
        {
            _network = network;
            _fullHierarchy = fullHierarchy;
        }

        public string Name => _network.Name;

        public int Id => _network.Id;

        public XDocument ConvertToXml(IEnumerable<DataBlock> queryParamers, Period defaultPeriod, string path)
        {
            var data = new XDocument();
            var rootElement = new XElement(RootName);
            var tree = _network
                .MyNodes
                .Where(n => IsNodeInPeriod(n, defaultPeriod) && n.LinkedObject != null) // мы грузим все node для Network, но некторые из них не ссылаются на LinkedObject потому что соотвествующие объекты не были запрошены
                .GenerateTree(n => n.NodeName, n => n.MyParent?.NodeName);

            if (!string.IsNullOrEmpty(path) && path != "*")
            {
                tree = tree.Where(n => n.Item.InPath(path));
            }

            rootElement.Add(GetSubElements(tree, queryParamers));
            data.Add(rootElement);

            return data;
        }

        public void LoadFromXml(XDocument source, XDocument target, Period defaultPeriod)
        {
            var updater = new NetworkTreeUpdater(_network, _fullHierarchy, defaultPeriod);
            updater.UpdateFromSourceToTarget(source, target);
        }

        public IEnumerable<VariableGroup> ConvertToGroups(IEnumerable<DataBlock> queryParamers, Period defaultPeriod, string path)
        {
            yield return new VariableGroup
            {
                Caption = "Test"//,
                //Values = 
            };
        }

        private static bool IsNodeInPeriod(NetworkTreeNode node, Period period)
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

            return false;
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
                using (var context = ModelsDescription.GetModel(_network.Solution.ModelName))
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
            using (var context = ModelsDescription.GetModel(_network.Solution.ModelName))
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
            using (var context = ModelsDescription.GetModel(_network.Solution.ModelName))
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
            foreach (var groupOfTreeItemsWithTheSameType in currentItems.GroupBy(c => c.Item.LinkedObject.ObjectModel.Name))
            {
                foreach (var treeItem in groupOfTreeItemsWithTheSameType)
                {
                    var newElement = new XElement(groupOfTreeItemsWithTheSameType.Key);
                    var add = false;
                    //var visible = false;
                    foreach (var qp in queryParamers.Where(q => q.RefObject == groupOfTreeItemsWithTheSameType.Key && q.Visible))
                    {
                        add = ProcessObjectElement(newElement, treeItem.Item, qp);
                        //visible |= qp.Visible; // если все блоки visible то добавляем новый элемент, иначе не вставляем этот элемент и переходим к его дочерним элементам
                        if(!add)
                        {
                            break;
                        }
                    }
                    //if (add)
                    //{
                        if (add)
                        {
                            newElement.Add(GetSubElements(treeItem.Children, queryParamers));
                            result.Add(newElement);
                        }
                        else
                        {
                            result.AddRange(GetSubElements(treeItem.Children, queryParamers));
                        }
                    //}
                }
            }
            return result;
        }

        private static bool ProcessObjectElement(XElement newElement, NetworkTreeNode node, DataBlock qp)
        {
            if (qp is NodeDataBlock)
            {
                var nsf = (NodeDataBlock)qp;
                switch (nsf.NodeType)
                {
                    case NodeType.UniqueName:
                        if(nsf.ObjectName != null && node.LinkedObject.Name != nsf.ObjectName)
                        {
                            return false;
                        }
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
                var variableElementName = VariableNameParser.GetVariableElementName(vsf);
                var variableElementValue = node.LinkedObject.GetVariableValue(varModel, qp.VariablePeriod);
                newElement.Add(new XElement(variableElementName, variableElementValue));
            }
            return true;
        }

    }
}