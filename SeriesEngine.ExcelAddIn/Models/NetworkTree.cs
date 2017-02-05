using AutoMapper;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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

        public string Name
        {
            get
            {
                return _network.Name;
            }
        }

        public string SolutionName
        {
            get
            {
                return _network.Solution.Name;
            }
        }

        public XDocument ConvertToXml(IEnumerable<DataBlock> queryParamers)
        {
            var data = new XDocument();
            var rootElement = new XElement(RootName);
            var tree = _network
                .Nodes
                .Cast<NetworkTreeNode>()
                .GenerateTree(n => n.NodeName, n => n.Parent?.NodeName);

            rootElement.Add(GetSubElements(tree, queryParamers));
            data.Add(rootElement);

            return data;
        }

        public void LoadFromXml(XDocument target)
        {
            var currentTreeState = _network
                .Nodes
                .Cast<NetworkTreeNode>()
                .GenerateTree(n => n.NodeName, n => n.Parent?.NodeName);

            using (var context = new Model1())
            {
                var allNodes = new List<NetworkTreeNode>(_network.Nodes);
                context.Solutions.Attach(_network.Solution);
                RestoreNodes(target.Root.Elements(), null, allNodes, context);

                var networkId = new SqlParameter("@networkId", _network.Id);
                context.Database.ExecuteSqlCommand("CleanNetwork @networkId", networkId);
                       
                context.SaveChanges();
            }
        }

        private void RestoreNodes(IEnumerable<XElement> elements, MainHierarchyNode parent, List<NetworkTreeNode> allNodes, Model1 context)
        {
            foreach(var element in elements)
            {
                // try to find object for node
                var nameAttr = element.Attribute("UniqueName");
                var sinceAttr = element.Attribute("Since");
                var tillAttr = element.Attribute("Till");

                var validFrom = sinceAttr == null ? (DateTime?)null : DateTime.Parse(sinceAttr.Value);
                var validTill = sinceAttr == null ? (DateTime?)null : DateTime.Parse(tillAttr.Value);

                if (nameAttr == null)
                {
                    parent.LinkedObject.SetVariableValue(element.Name.LocalName, element.Value);
                }
                else
                {
                    //var node = _network.Nodes.FirstOrDefault(n => n.NodeName == nameAttr.Value && n.Parent == parent);
                    //if (node == null)
                    //{
                        var node = new MainHierarchyNode
                        {
                            Network = _network,
                            Parent = parent,
                            ValidFrom = validFrom,
                            ValidTill = validTill
                        };
                        // create a new object and node
                        node.SetLinkedObject(CreateObject(element));
                        context.MainHierarchyNodes.Add(node);
                    //}
                    //else
                    //{
                    //    allNodes.Remove(node);
                    //}
                    RestoreNodes(element.Elements(), node, allNodes, context);
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
                        Solution = _network.Solution,
                        Name = objName
                    };
                case "Consumer":
                    return new Consumer
                    {
                        ObjectModel = MainHierarchyNode.ConsumerModel,
                        Solution = _network.Solution,
                        Name = objName
                    };
                case "Contract":
                    return new Contract
                    {
                        ObjectModel = MainHierarchyNode.ContractModel,
                        Solution = _network.Solution,
                        Name = objName
                    };
                case "ConsumerObject":
                    return new ConsumerObject
                    {
                        ObjectModel = MainHierarchyNode.ConsumerObjectModel,
                        Solution = _network.Solution,
                        Name = objName
                    };
                case "Point":
                    return new Point
                    {
                        ObjectModel = MainHierarchyNode.PointModel,
                        Solution = _network.Solution,
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
            IEnumerable<DataBlock> queryParamers)
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

        private static void ProcessObjectElement(XElement newElement, NetworkTreeNode node, DataBlock qp)
        {
            if (qp is NodeDataBlock)
            {
                var nsf = (NodeDataBlock)qp;
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
            else if (qp is VariableDataBlock) // переменные только для данного объекта
            {
                var vsf = (VariableDataBlock)qp;
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
