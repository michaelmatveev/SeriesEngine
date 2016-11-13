using SeriesEngine.ExcelAddIn.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class NetworkTree : Network
    {
        public List<NetworkTreeNode> Nodes { get; } = new List<NetworkTreeNode>();

        public XDocument ConvertToXml(IEnumerable<SubFragment> queryParamers)
        {
            var data = new XDocument();
            var rootElement = new XElement("DataToImport");

            var tree = Nodes.GenerateTree(n => n.NodeName, n => n.Parent?.NodeName);
            rootElement.Add(GetSubElements(tree, queryParamers));
            data.Add(rootElement);

            return data;
        }

        public void LoadFromXml(XDocument document)
        {

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
                        newElement.Add(new XAttribute("Since", node.Since));
                        break;
                    case NodeType.Till:
                        newElement.Add(new XAttribute("Till", node.Till));
                        break;
                    default:
                        throw new NotSupportedException("this operation is not supported");
                }
            }
            else if (qp is VariableSubFragment) // переменные только для данного объекта
            {
                var vsf = (VariableSubFragment)qp;
                newElement.Add(new XElement(vsf.VariableName, node.LinkedObject[vsf.VariableName]));
            }
        }
    }
}
