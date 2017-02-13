using AutoMapper;
using SeriesEngine.ExcelAddIn.Helpers;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;
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

        public XDocument ConvertToXml(IEnumerable<DataBlock> queryParamers, Period defaultPeriod)
        {
            var data = new XDocument();
            var rootElement = new XElement(RootName);
            var tree = _network
                .Nodes
                .Cast<NetworkTreeNode>()
                .ToList()
                .Where(n => IsNodeInPeriod(n, defaultPeriod))
                .GenerateTree(n => n.NodeName, n => n.Parent?.NodeName);

            rootElement.Add(GetSubElements(tree, queryParamers));
            data.Add(rootElement);

            return data;
        }

        private static bool IsNodeInPeriod(NetworkTreeNode node, Period period)
        {
            if(!(node.ValidFrom.HasValue || node.ValidTill.HasValue))
            {
                return true;
            }
            return (node.ValidTill ?? DateTime.MinValue) >= period.From && (node.ValidFrom ?? DateTime.MaxValue) < period.Till;
            //return (node.ValidFrom ?? DateTime.MaxValue) >= period.From && (node.ValidTill ?? DateTime.MinValue) < period.Till;
        }

        public void LoadFromXml(XDocument target)
        {
            using (var context = new Model1())
            {
                var allNodes = new List<NetworkTreeNode>(_network.Nodes);
                context.Solutions.Attach(_network.Solution);

                var newNodes = RestoreNodes(target.Root.Elements(), null, allNodes);
                context.MainHierarchyNodes.AddRange(newNodes.Cast<MainHierarchyNode>());
                context.MainHierarchyNodes.RemoveRange(allNodes.Where(n => !n.IsMarkedFlag).Cast<MainHierarchyNode>());
                      
                context.SaveChanges();
            }
        }

        private IEnumerable<NetworkTreeNode> RestoreNodes(IEnumerable<XElement> elements, NetworkTreeNode parent, List<NetworkTreeNode> allNodes)
        {
            var result = new List<NetworkTreeNode>();
            // elements at one hierarchy level
            foreach (var element in elements)
            {
                // try to find object for node
                var nameAttr = element.Attribute("UniqueName");
                if (nameAttr == null)
                {
                    parent.LinkedObject.SetVariableValue(element.Name.LocalName, element.Value);
                }
                else
                {
                    var validFrom = DateTimeParser.Parse(element.Attribute("Since")?.Value);
                    var validTill = DateTimeParser.Parse(element.Attribute("Till")?.Value);

                    var node = allNodes.FirstOrDefault(n => n.NodeName == nameAttr.Value && n.Parent == parent);
                    if (node == null) // it is a new node
                    {
                        node = new MainHierarchyNode
                        {
                            IsMarkedFlag = true,
                            Network = _network,
                            Parent = (MainHierarchyNode)parent,
                            ValidFrom = validFrom,
                            ValidTill = validTill
                        };
                        // create a new object and node
                        node.SetLinkedObject(CreateObject(element));
                        result.Add(node);
                    }
                    else
                    {
                        node.IsMarkedFlag = true;
                        node.ValidFrom = validFrom;
                        node.ValidTill = validTill;
                    }
                    if (element.Elements().Any())
                    {
                        result.AddRange(RestoreNodes(element.Elements(), node, allNodes));
                    }
                }
            }
            return result;
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
