﻿using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.msk1;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

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
                .Where(n => IsNodeInPeriod(n, defaultPeriod, false))
                .GenerateTree(n => n.NodeName, n => n.MyParent?.NodeName);

            rootElement.Add(GetSubElements(tree, queryParamers));
            data.Add(rootElement);

            return data;
        }

        public void LoadFromXml(XDocument source, XDocument target)
        {
            var nodes = new List<NetworkTreeNode>();
            nodes.AddRange(ProcessNodesElements(source, null, target.Root.Elements()));
            nodes.AddRange(FindNodesToDelete(source.Root.Elements()));
            //Update(_network.MyNodes);
            Update(nodes);
        }
        
        private static string GetXPath(XElement element)
        {
            if(element == null)
            {
                return string.Empty;
            }
            else
            {
                return $"{GetXPath(element.Parent)}/{element.Name}";
            }
        }

        private IList<NetworkTreeNode> ProcessNodesElements(XDocument source, NetworkTreeNode parent, IEnumerable<XElement> elements)
        {
            var result = new List<NetworkTreeNode>();
            foreach (var element in elements.Where(e => e.Attribute("UniqueName") != null))
            {
                NetworkTreeNode node;
                var path = $"{GetXPath(element)}[@UniqueName='{element.Attribute("UniqueName").Value}']";
                var sourceElement = source.XPathSelectElement(path);
                if (sourceElement == null)
                {
                    // this is new node
                    node = CreateNode(element, parent);
                    result.Add(node);
                    //_network.MyNodes.Add(node);
                }
                else
                {
                    // this is already existed node
                    var id = int.Parse(sourceElement.Attribute("NodeId").Value);
                    node = _network.MyNodes.Single(n => n.Id == id);
                    UpdateNode(node, element, parent);
                    //_network.MyNodes.Add(node);
                    result.Add(node);
                    sourceElement.Attribute("NodeId").Value = "0"; // признак того что элемент обработан
                }
                result.AddRange(ProcessNodesElements(source, node, element.Elements()));
            }
            return result;
        }

        private IList<NetworkTreeNode> FindNodesToDelete(IEnumerable<XElement> elements)
        {
            var result = new List<NetworkTreeNode>();
            foreach (var element in elements.Where(e => e.Attribute("NodeId") != null))
            {
                var attr = element.Attribute("NodeId");
                var id = int.Parse(attr.Value);
                if (id != 0)
                {
                    var node = _network.MyNodes.Single(n => n.Id == id);
                    node.State = ObjectState.Deleted;
                    result.Add(node);
                }
                result.AddRange(FindNodesToDelete(element.Elements()));
            }
            return result;
        }

        private NetworkTreeNode CreateNode(XElement element, NetworkTreeNode parent)
        {
            var validFrom = ParseDateTimeString(element.Attribute("Since")?.Value);
            var validTill = ParseDateTimeString(element.Attribute("Till")?.Value);

            var node = (NetworkTreeNode)Activator.CreateInstance(_network.HierarchyModel.NodeType);
            node.MyNetwork = _network;
            node.MyParent = parent;
            node.ValidFrom = validFrom;
            node.ValidTill = validTill;
            node.State = ObjectState.Added;
            node.SetLinkedObject(CreateObject(element));
            node.LinkedObject.State = ObjectState.Added;
                        
            foreach (var v in element.Elements())
            {
                if(v.Attribute("UniqueName") == null)
                {
                    node.LinkedObject.SetVariableValue(v.Name.LocalName, v.Value);
                }
            }
            return node;
        }

        private void UpdateNode(NetworkTreeNode node, XElement element, NetworkTreeNode parent)
        {
            var validFrom = ParseDateTimeString(element.Attribute("Since")?.Value);
            var validTill = ParseDateTimeString(element.Attribute("Till")?.Value);

            node.MyNetwork = _network;
            node.MyParent = parent;
            if (node.ValidFrom != validFrom || node.ValidTill != validTill)
            {
                node.ValidFrom = validFrom;
                node.ValidTill = validTill;
                node.State = ObjectState.Modified;
            }

            var linkedObjectUpdated = false; // check that at least one field has been updated
            foreach (var v in element.Elements().Where(v => v.Attribute("UniqueName") == null))
            {
                if(node.LinkedObject.SetVariableValue(v.Name.LocalName, v.Value) && !linkedObjectUpdated)
                {
                    linkedObjectUpdated = true;
                }
            }

            if(linkedObjectUpdated)
            {
                node.LinkedObject.State = ObjectState.Modified;
            }
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
                .Where(n => n.LinkedObject.ObjectModel.Name == objectTypeName)
                .SingleOrDefault(n => n.NodeName == name)
                ?.LinkedObject;
        }

        private DateTime? ParseDateTimeString(string value)
        {
            return string.IsNullOrEmpty(value) ? new DateTime?() : DateTime.Parse(value);
        }

        private NamedObject CreateObject(XElement element)
        {
            var objName = element.Attribute("UniqueName").Value;
            var objectModel = ModelsDescription
                .All
                .Single(m => m.Name == _network.Solution.ModelName)
                .ObjectModels
                .Single(om => om.Name == element.Name.LocalName);

            var objectInstance = (NamedObject)Activator.CreateInstance(objectModel.ObjectType);
            //objectInstance.Solution = _network.Solution;
            objectInstance.SolutionId = _network.SolutionId;
            objectInstance.Name = objName;
            objectInstance.State = ObjectState.Added;

            return objectInstance;
        }

        public void Update(IEnumerable<IStateObject> valuesForPeriod)
        {
            using (var context = new Model1())
            {
                //context.Configuration.AutoDetectChangesEnabled = false;
                context.Networks.Attach(_network);
                foreach(var v in valuesForPeriod)
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

        //private NamedObject FindNamedObject(Model1 context, XElement element)
        //{
        //    var objectType = $"{element.Name.LocalName}s";
        //    var objectName = element.Attribute("UniqueName").Value;

        //    var property = context.GetType().GetProperty(objectType);
        //    dynamic dbSet = property.GetValue(context);

        //    var prmSolutionId = new SqlParameter("@SolutionId", _network.SolutionId);
        //    var prmObjectName = new SqlParameter("@ObjectName", objectName);

        //    var entries = dbSet.SqlQuery($"SELECT * FROM pwk1.[{objectType}] where SolutionId=@SolutionId and Name=@ObjectName", prmSolutionId, prmObjectName);
        //    foreach (NamedObject d in entries)
        //    {
        //        d.ObjectModel = MainHierarchyNode.GetObjectModelByName(element.Name.LocalName);
        //        return d;
        //    }
        //    return null;
        //}

        public void RenameObjectLinkedWithNode(int nodeId, string newName)
        {
            using (var context = new Model1())
            {
                //var node = context.MainHierarchyNodes.Find(nodeId);
                //node.LinkedObject.SetName(newName);

                var node = _network.MyNodes.FirstOrDefault(n => n.Id == nodeId);
                context.Entry(node).State = System.Data.Entity.EntityState.Unchanged;
                node.LinkedObject.SetName(newName);

                context.SaveChanges();
            }
        }

        public void DeleteObjectLinkedWithNode(int nodeId)
        {
            using (var context = new Model1())
            {
                context.Database.Log = (s) => Debug.WriteLine(s);

                var nodeSetPropertyName = $"{_network.HierarchyModel.Name}Nodes";
                var nodeSetProperty = context.GetType().GetProperty(nodeSetPropertyName);
                dynamic nodeSet = nodeSetProperty.GetValue(context);
                var node = nodeSet.Find(nodeId);
                //var node = context.MainHierarchyNodes.Find(nodeId);
                dynamic lo = node.LinkedObject;
                var paramId = new SqlParameter("@Id", lo.Id);
                var paramTs = new SqlParameter("@ConcurrencyStamp_Original", lo.ConcurrencyStamp);
                var spName = $"msk1.{node.LinkedObject.ObjectModel.Name}_Delete";
                context.Database.ExecuteSqlCommand($"exec {spName} @Id, @ConcurrencyStamp_Original", paramId, paramTs);
                //context.Entry(node.LinkedObject).State = System.Data.Entity.EntityState.Deleted;
                //context.SaveChanges();
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

        public IDisposable GetExportLock(CollectionDataBlock cb)
        {
            //TODO think about locking
            //if(_network.Revision > cb.NetworkRevision)
            //{
            //    throw new Exception($"Данные в коллекции '{cb.Name}' отличаются от представленных на листе.");
            //}

            return new Disposable(() =>
            {
                using (var context = new Model1())
                {
                    context.Networks.Attach(_network);
                    _network.Revision += 1;
                    context.SaveChanges();
                    cb.NetworkRevision = _network.Revision;
                }
            });
        }

        public IDisposable GetImportLock(CollectionDataBlock cb)
        {
            return new Disposable(() =>
            {
                cb.NetworkRevision = _network.Revision;
            });
        }

    }
}