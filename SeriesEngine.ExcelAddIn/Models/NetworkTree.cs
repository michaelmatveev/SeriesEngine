using AutoMapper;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public string Name
        {
            get
            {
                return _network.Name;
            }
        }

        public XDocument ConvertToXml(IEnumerable<SubFragment> queryParamers)
        {
            var data = new XDocument();
            var rootElement = new XElement(RootName);

            var tree = _network.Nodes.Cast<NetworkTreeNode>().GenerateTree(n => n.NodeName, n => n.Parent?.NodeName);
            rootElement.Add(GetSubElements(tree, queryParamers));
            data.Add(rootElement);

            return data;
        }

        public void LoadFromXml(IEnumerable<SubFragment> queryParamers, XDocument target)
        {
            var currentTreeState = _network
                .Nodes
                .Cast<NetworkTreeNode>()
                .GenerateTree(n => n.NodeName, n => n.Parent?.NodeName);
            using (var context = new Model1())
            {
                var nodesToDelete = new List<NetworkTreeNode>();
                DeleteNodes(currentTreeState, context, nodesToDelete);
                RestoreNodes(target.Root.Elements(), null, nodesToDelete, context);
                foreach(var n in nodesToDelete)
                {
                    context.Entry(n).State = EntityState.Deleted;
                }        
                context.SaveChanges();
            }
        }

        private void DeleteNodes(IEnumerable<TreeItem<NetworkTreeNode>> treeItems, Model1 context, List<NetworkTreeNode> nodesToDelete)
        {
            foreach(var c in treeItems)
            {
                nodesToDelete.Add(c.Item);
                //context.Entry(c.Item).State = EntityState.Deleted;
                DeleteNodes(c.Children, context, nodesToDelete);
            }
        }

        private void RestoreNodes(IEnumerable<XElement> elements, MainHierarchyNode parent, List<NetworkTreeNode> nodesToDelete, Model1 context)
        {
            foreach(var element in elements)
            {
                // try to find object for node
                var nameAttr = element.Attribute("UniqueName");
                var sinceAttr = element.Attribute("Since");
                var tillAttr = element.Attribute("Till");

                if (nameAttr != null)
                {
                    var node = _network.Nodes.FirstOrDefault(n => n.NodeName == nameAttr.Value && n.Parent == parent);
                    if (node == null)
                    {
                        node = new MainHierarchyNode
                        {
                            Network = _network,
                            Parent = parent
                        };
                        // create a new object and node
                        node.SetLinkedObject(CreateObject(element));
                        context.MainHierarchyNodes.Add(node);
                    }
                    else
                    {
                        nodesToDelete.Remove(node);
                        // object exists for the same node
                        //context.Entry(node).State = EntityState.Unchanged;
                    }
                    RestoreNodes(element.Elements(), node, nodesToDelete, context);
                }
            }
        }

        public NamedObject CreateObject(XElement element)
        {
            var objName = element.Attribute("UniqueName").Value;
            switch (element.Name.LocalName)
            {
                case "Region":
                    return new Region
                    {
                        ObjectModel = MainHierarchyNode.RegionModel,
                        Name = objName
                    };
                case "Consumer":
                    return new Consumer
                    {
                        ObjectModel = MainHierarchyNode.ConsumerModel,
                        Name = objName
                    };
                case "Contract":
                    return new Contract
                    {
                        ObjectModel = MainHierarchyNode.ContractModel,
                        Name = objName
                    };
                case "ConsumerObject":
                    return new ConsumerObject
                    {
                        ObjectModel = MainHierarchyNode.ConsumerObjectModel,
                        Name = objName
                    };
                case "Point":
                    return new Point
                    {
                        ObjectModel = MainHierarchyNode.PointModel,
                        Name = objName
                    };

                default: throw new NotSupportedException();
            }
        }

        //public void LoadFromXml(IEnumerable<SubFragment> queryParamers, XDocument target)
        //{
        //    // TODO find diffrence
        //    XDocument source = ConvertToXml(queryParamers);
        //    // get current state of network
        //    var currentTreeState = _network.Nodes.GenerateTree(n => n.NodeName, n => n.Parent?.NodeName);
        //    using (var context = new Model1())
        //    {
        //        context.Networks.Attach(_network);
        //        foreach (var c in currentTreeState)
        //        {
        //            // try to find node that corresponds target
        //            var currentObject = c.Item.LinkedObject;
        //            var element = target.XPathSelectElement($"/{RootName}/Region[@UniqueName='{c.Item.NodeName}']");
        //            if (element == null) // cannot find element in target, have to delete node
        //            {
        //                //context.MainHierarchyNodes.Attach(c.Item);
        //                context.Entry(c.Item).State = System.Data.Entity.EntityState.Deleted;
        //            }
        //        }

        //        foreach(var x in target.Root.Descendants())
        //        {

        //        }

        //        //context.SaveChanges();
        //    }

        //    //using (var context = new Model1())
        //    //{
        //    //    foreach (var re in target.Root.Elements("Region"))
        //    //    {
        //    //        var newNode = new MainHierarchyNode()
        //    //        {
        //    //            Region = new Region()
        //    //            {
        //    //                Name = re.Attribute("UniqueName").Value
        //    //            }
        //    //        };

        //    //        _network.Nodes.Add(newNode);
        //    //    }


        //    //    context.SaveChanges();
        //    //}


        //}

        private IEnumerable<XElement> GetSubElements(
            IEnumerable<TreeItem<NetworkTreeNode>> currentItems,
            IEnumerable<SubFragment> queryParamers)
        {
            var result = new List<XElement>();
            foreach(var groupOfSameObjects in currentItems.GroupBy(c => c.Item.LinkedObject.ObjectModel.Name))
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

        private static void ProcessObjectElement(XElement newElement, NetworkTreeNode node, SubFragment qp)
        {
            if (qp is NodeSubFragment)
            {
                var nsf = (NodeSubFragment)qp;
                switch (nsf.NodeType)
                {
                    case NodeType.UniqueName:
                        newElement.Add(new XAttribute("UniqueName", node.NodeName));
                        break;
                    case NodeType.Since:
                        newElement.Add(new XAttribute("Since", node.ValidFrom ?? default(DateTime)));
                        break;
                    case NodeType.Till:
                        newElement.Add(new XAttribute("Till", node.ValidTill ?? default(DateTime)));
                        break;
                    default:
                        throw new NotSupportedException("this operation is not supported");
                }
            }
            else if (qp is VariableSubFragment) // переменные только для данного объекта
            {
                var vsf = (VariableSubFragment)qp;
                newElement.Add(new XElement(vsf.VariableName, node.LinkedObject.GetVariableValue(vsf.VariableName)));
            }
        }
    }

    public class XElementResolver<D, T> : IValueResolver<XElement, D, T>
    {
        public T Resolve(XElement source, D destination, T destMember, ResolutionContext context)
        {
            if (source == null || string.IsNullOrEmpty(source.Value))
                return default(T);
            return (T)Convert.ChangeType(source.Value, typeof(T));
        }
    }

    public class XAttributeResolver<D, T> : IValueResolver<XElement, D, T>
    {
        public XAttributeResolver(string attributeName)
        {
            Name = attributeName;
        }

        public string Name { get; set; }

        public T Resolve(XElement source, D destination, T destMember, ResolutionContext context)
        {
            if (source == null)
                return default(T);
            var attribute = source.Attribute(Name);
            if (attribute == null || String.IsNullOrEmpty(attribute.Value))
                return default(T);

            return (T)Convert.ChangeType(attribute.Value, typeof(T));
        }
    }
}
