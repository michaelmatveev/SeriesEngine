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
        private const string FormualElementName = "FFragment";
        private const string PeriodElemenentName = "Period";

        public static BaseDataBlock GetDataBlock(XDocument source, Period defaultPeriod)
        {
            var interval = (TimeInterval)Enum.Parse(typeof(TimeInterval), source.Root.Attribute("Interval")?.Value ?? "None");
            var result = new CollectionDataBlock
            {
                CustomPeriod = defaultPeriod,
                NetworkName = source.Root.Attribute("NetworkName").Value,
                NetworkRevision = int.Parse(source.Root.Attribute("NetworkRevision")?.Value ?? "0"),
                Name = source.Root.Attribute("Name").Value,
                Sheet = source.Root.Attribute("Sheet").Value,
                Cell = source.Root.Attribute("Cell").Value,
                AddIndexColumn = bool.Parse(source.Root.Attribute("AddIndexColumn")?.Value ?? "True"),
                ShowHeader = bool.Parse(source.Root.Attribute("ShowHeader")?.Value ?? "True"),
                CustomPath = source.Root.Attribute("CustomPath")?.Value ?? string.Empty,
                Interval = interval
            };

            if (interval != TimeInterval.None && interval != TimeInterval.Indefinite)
            {
                var periodDataBlock = new PeriodDataBlock(result)
                {
                    PeriodInterval = interval,
                    Level = 0,
                    Caption = "Период"
                };
                result.DataBlocks.Add(periodDataBlock);
            }

            var model = source.Root.Attribute("Model").Value;
            foreach (var f in source.Root.Descendants())
            {
                DataBlock newDataBlock;
                var objectType = f.Attribute("RefObject")?.Value;
                if (f.Name.LocalName == NodeElementName)
                {
                    newDataBlock = new NodeDataBlock(result)
                    {
                        NodeType = (NodeType)Enum.Parse(typeof(NodeType), f.Attribute("Type").Value),
                        ObjectName = f.Attribute("ObjName")?.Value
                    };
                }
                else if(f.Name.LocalName == VariableElementName)
                {
                    var objectModel = ModelsDescription
                        .All
                        .Single(m => m.Name == model)
                        .ObjectModels
                        .Single(om => om.Name == objectType);

                    var variableType = f.Attribute("Variable").Value;
                    newDataBlock = new VariableDataBlock(result)
                    {
                        Kind = (Kind)Enum.Parse(typeof(Kind), f.Attribute("Kind").Value),
                        VariableMetamodel = objectModel.Variables.Single(v => v.Name == variableType)
                    };
                }
                else if(f.Name.LocalName == PeriodElemenentName)
                {
                    newDataBlock = new PeriodDataBlock(result);
                }
                else
                {
                    newDataBlock = new FormulaDataBlock(result)
                    {
                        Formula = f.Attribute("Formula").Value
                    };
                }

                newDataBlock.Caption = f.Attribute("Caption").Value;
                newDataBlock.Level = int.Parse(f.Attribute("Level").Value);
                newDataBlock.RefObject = objectType;
                newDataBlock.Visible = bool.Parse(f.Attribute("Visible")?.Value ?? "True");
                newDataBlock.Shift = int.Parse(f.Attribute("Shift")?.Value ?? "0");
                result.DataBlocks.Add(newDataBlock);
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
                        new XAttribute("Model", "msk1"), //TODO replace with current solution model
                        new XAttribute("Cell", coll.Cell),
                        new XAttribute("AddIndexColumn", coll.AddIndexColumn),
                        new XAttribute("ShowHeader", coll.ShowHeader),
                        new XAttribute("CustomPath", coll.CustomPath),                        
                        new XAttribute("Interval", coll.Interval));
                var doc = new XDocument(root);

                foreach(var n in coll.DataBlocks)
                {
                    XElement newElement;
                    if (n is NodeDataBlock)
                    {
                        var ndb = n as NodeDataBlock;
                        newElement = new XElement(ns + NodeElementName,
                            new XAttribute("Type", ndb.NodeType),
                            new XAttribute("RefObject", n.RefObject));
                    }
                    else if (n is VariableDataBlock)
                    {
                        var vdb = n as VariableDataBlock;
                        newElement = new XElement(ns + VariableElementName,
                            new XAttribute("Variable", vdb.VariableMetamodel.Name),
                            new XAttribute("Kind", vdb.Kind),
                            new XAttribute("Shift", vdb.Shift),
                            new XAttribute("RefObject", n.RefObject));
                    }
                    else if (n is PeriodDataBlock)
                    {
                        newElement = new XElement(ns + "Period");
                    }
                    else
                    {
                        var fdb = n as FormulaDataBlock;
                        newElement = new XElement(ns + FormualElementName,
                            new XAttribute("Formula", fdb.Formula));
                    }
                    newElement.Add(new XAttribute("Caption", n.Caption));
                    newElement.Add(new XAttribute("Level", n.Level));

                    root.Add(newElement);
                }
                return doc;
            }

            throw new NotSupportedException();
        }
    }
}
