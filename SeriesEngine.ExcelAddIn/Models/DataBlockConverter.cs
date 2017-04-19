using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Models
{
    public static class DataBlockConverter
    {
        private const string NodeElementName = "CFragment";
        private const string VariableElementName = "VFragment";

        public static BaseDataBlock GetDataBlock(XDocument source, Period defaultPeriod)
        {
            var result = new CollectionDataBlock
            {
                CustomPeriod = defaultPeriod,
                NetworkName = source.Root.Attribute("NetworkName").Value,
                NetworkRevision = int.Parse(source.Root.Attribute("NetworkRevision")?.Value ?? "0"),
                Name = source.Root.Attribute("Name").Value,
                Sheet = source.Root.Attribute("Sheet").Value,
                Cell = source.Root.Attribute("Cell").Value,
            };

            var model = source.Root.Attribute("Model").Value;
            foreach (var f in source.Root.Descendants())
            {
                DataBlock newFragment;
                var objectType = f.Attribute("RefObject").Value;
                if (f.Name.LocalName == NodeElementName)
                {
                    newFragment = new NodeDataBlock(result)
                    {
                        NodeType = (NodeType)Enum.Parse(typeof(NodeType), f.Attribute("Type").Value)
                    };
                }
                else
                {
                    var objectModel = ModelsDescription
                        .All
                        .Single(m => m.Name == model)
                        .ObjectModels
                        .Single(om => om.Name == objectType);

                    var variableType = f.Attribute("Variable").Value;
                    newFragment = new VariableDataBlock(result)
                    {
                        Kind = (Kind)Enum.Parse(typeof(Kind), f.Attribute("Kind").Value),
                        VariableMetamodel = objectModel.Variables.Single(v => v.Name == variableType)
                    };
                }

                newFragment.Caption = f.Attribute("Caption").Value;
                newFragment.Level = Int32.Parse(f.Attribute("Level").Value);
                newFragment.CollectionName = f.Attribute("CollectionName").Value;
                newFragment.RefObject = objectType;

                result.DataBlocks.Add(newFragment);
            }
            return result;
        }

        public static XDocument GetXml(BaseDataBlock datablock)
        {
            if (datablock is CollectionDataBlock)
            {
                var coll = (CollectionDataBlock)datablock;    
                var ns = XNamespace.Get(Constants.XmlNamespaceDataBlocks);
                var root = new XElement(ns + "ObjectGrid",
                        new XAttribute("Version", "1"),
                        new XAttribute("NetworkName", coll.NetworkName),
                        new XAttribute("NetworkRevision", coll.NetworkRevision),
                        new XAttribute("Name", coll.Name),
                        new XAttribute("Sheet", coll.Sheet),
                        new XAttribute("Model", "msk1"),
                        new XAttribute("Cell", coll.Cell));
                var doc = new XDocument(root);

                foreach(var n in coll.DataBlocks)
                {
                    XElement newElement;
                    if(n is NodeDataBlock)
                    {
                        var ndb = n as NodeDataBlock;
                        newElement = new XElement(NodeElementName,
                            new XAttribute("Type", ndb.NodeType));
                    }
                    else
                    {
                        var vdb = n as VariableDataBlock;
                        newElement = new XElement(VariableElementName,
                            new XAttribute("Variable", vdb.VariableMetamodel.Name),
                            new XAttribute("Kind", vdb.Kind));
                    }
                    newElement.Add(new XAttribute("Caption", n.Caption));
                    newElement.Add(new XAttribute("Level", n.Level));
                    newElement.Add(new XAttribute("CollectionName", n.CollectionName));
                    newElement.Add(new XAttribute("RefObject", n.RefObject));

                    root.Add(newElement);
                }
                return doc;
            }

            throw new NotSupportedException();
        }
    }
}
