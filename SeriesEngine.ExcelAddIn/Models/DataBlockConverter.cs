using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Models
{
    public static class DataBlockConverter
    {
        private const string XmlNamespace = "http://www.seriesengine.com/SeriesEngine.ExcelAddIn/GridFragments";

        public static BaseDataBlock GetDataBlock(XDocument source, Period defaultPeriod)
        {
            var result = new CollectionDataBlock
            {
                CustomPeriod = defaultPeriod,
                NetworkName = source.Root.Attribute("NetworkName").Value,
                Name = source.Root.Attribute("Name").Value,
                Sheet = source.Root.Attribute("Sheet").Value,
                Cell = source.Root.Attribute("Cell").Value,
            };

            foreach (var f in source.Root.Descendants())
            {
                DataBlock newFragment;
                if(f.Name.LocalName == "CFragment")
                {
                    newFragment = new NodeDataBlock(result)
                    {
                        NodeType = (NodeType)Enum.Parse(typeof(NodeType), f.Attribute("Type").Value)
                    };
                }
                else
                {
                    newFragment = new VariableDataBlock(result)
                    {
                        Kind = (Kind)Enum.Parse(typeof(Kind), f.Attribute("Kind").Value),
                        VariableName = f.Attribute("Variable").Value
                    };
                }

                newFragment.Caption = f.Attribute("Caption").Value;
                newFragment.Level = Int32.Parse(f.Attribute("Level").Value);
                newFragment.CollectionName = f.Attribute("CollectionName").Value;
                newFragment.RefObject = f.Attribute("RefObject").Value;

                result.DataBlocks.Add(newFragment);
            }
            return result;
        }

        public static XDocument GetXml(BaseDataBlock datablock)
        {
            if (datablock is CollectionDataBlock)
            {
                var coll = (CollectionDataBlock)datablock;    
                var ns = XNamespace.Get(XmlNamespace);
                var doc = new XDocument(
                    new XElement(ns + "ObjectGrid",
                        new XAttribute("Version", "1"),
                        new XAttribute("Name", coll.Name),
                        new XAttribute("Sheet", coll.Sheet),
                        new XAttribute("Cell", coll.Cell)));

                return doc;
            }

            throw new NotSupportedException();
        }
    }
}
