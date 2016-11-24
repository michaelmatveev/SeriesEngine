using AutoMapper;
using SeriesEngine.ExcelAddIn.Helpers;
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

        public XDocument ConvertToXml(IEnumerable<SubFragment> queryParamers)
        {
            var data = new XDocument();
            var rootElement = new XElement(RootName);

            var tree = _network.Nodes.Cast<NetworkTreeNode>().GenerateTree(n => n.NodeName, n => n.Parent?.NodeName);
            rootElement.Add(GetSubElements(tree, queryParamers));
            data.Add(rootElement);

            return data;
        }

        public void LoadFromXml(XDocument target)
        {
            // TODO find diffrence
            //XDocument source = ConvertToXml(queryParamers);

            using (var context = new Model1())
            {
                foreach (var re in target.Root.Elements("Region"))
                {
                    var newNode = new MainHierarchyNode()
                    {
                        Region = new Region()
                        {
                            Name = re.Attribute("UniqueName").Value
                        }
                    };

                    _network.Nodes.Add(newNode);
                }


                context.SaveChanges();
            }

            // Convert XML document into DB objects
            //Mapper.Initialize(cfg => 
            //{
            //    cfg.CreateMap<XElement, Network>()
            //        .ForMember(
            //            n => n.MainHierarchyNodes,
            //            opt => opt.MapFrom(src => src.Elements("Region").ToList()));

            //    cfg.CreateMap<XElement, MainHierarchyNode>()
            //        .ForMember(
            //            n => n.Parent,
            //            opt => opt.MapFrom(src => src.Parent));

            //    cfg.CreateMap<XElement, MainHierarchyNode>()
            //        .ForMember(
            //            n => n.Region,
            //            opt => opt.MapFrom(src => src));

            //    cfg.CreateMap<XElement, Region>()
            //        .ForMember(
            //            r => r.Name,
            //            opt => opt.ResolveUsing(new XAttributeResolver<Region, string>("UniqueName")));

            //});                    

            //var network = Mapper.Map<XElement, Network>(document.Root);
        }

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
