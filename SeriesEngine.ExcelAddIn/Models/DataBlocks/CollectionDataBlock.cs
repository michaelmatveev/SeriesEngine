﻿using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Models.DataBlocks
{
    public enum PeriodType
    {
        Common,
        Custom,
        //CommonWithShift,
    }

    [Serializable]
    public class CollectionDataBlock : SheetDataBlock
    {
        private static XNamespace ns = "http://www.w3.org/2001/XMLSchema";
        private static XNamespace msdata = "urn:schemas-microsoft-com:xml-msdata";

        public string NetworkName { get; set; }
        public int NetworkRevision { get; set; }
        public IEnumerable<ObjectMetamodel> SupportedModels { get; set; }
        public bool ShowHeader { get; set; } = true;
        public PeriodType PeriodType { get; set; } = PeriodType.Common;
        public Period CustomPeriod { get; set; }
        public XDocument Xml { get; set; }
        public bool AddIndexColumn { get; set; }
        public IList<DataBlock> DataBlocks { get; private set; } = new List<DataBlock>();

        public CollectionDataBlock() : this(null, Period.Default)
        {
        }

        public CollectionDataBlock(SheetDataBlock parent, Period defaultPeriod) : base(parent)
        {
            CustomPeriod = defaultPeriod;
            AddIndexColumn = true;
        }

        public override void Export(Solution solution, BaseDataExporter exproter)
        {
            if(this.Xml == null)
            {
                throw new InvalidOperationException($"Обновите блок данных {this.Name}");
            }
            exproter.ExportDataBlock(solution, this);
        }

        public override void Import(Solution solution, BaseDataImporter importer)
        {
            importer.ImportDataBlock(solution, this);
        }

        public string GetSchema()
        {
            var schema = new XDocument(
                new XElement(ns + "schema",
                    new XElement(ns + "element",
                        new XAttribute("name", NetworkTree.RootName),
                        new XAttribute("nillable", "true"),
                        new XElement(ns + "complexType",
                            new XElement(ns + "sequence", new XAttribute("minOccurs", "0"))))));

            schema.Root.SetAttributeValue(XNamespace.Xmlns + "xs", ns);
            var lastElement = schema.Descendants().Where(d => !d.HasElements).Single();
            var currentPath = $"/{NetworkTree.RootName}";

            // the most nested objects must have an "_Id" column dataset's table, that is not generated by default
            // therefore add extra one level
            var subFragmentsWithMostNestedStub = DataBlocks.Concat(new[] {
                new NodeDataBlock(this)
                {
                    Level = int.MaxValue,
                    NodeType = NodeType.UniqueName,
                    RefObject = "Stub"
                }
            });

            foreach (var sfGroup in subFragmentsWithMostNestedStub
                .GroupBy(sf => sf.Level)
                .OrderBy(sfg => sfg.Key))
            {
                var complexType = new XElement(ns + "complexType");
                var sequence = new XElement(ns + "sequence", new XAttribute("minOccurs", "0"));
                complexType.Add(sequence);

                foreach (var sf in sfGroup)
                {
                    if (!lastElement.Descendants(ns + "element").Any(d => d.Attribute("name").Value == sf.RefObject))
                    {
                        lastElement.Add(
                            new XElement(ns + "element",
                                new XAttribute("name", sf.RefObject),
                                new XAttribute("minOccurs", "0"),
                                new XAttribute("maxOccurs", "unbounded"),
                                new XAttribute("nillable", "true"),
                                new XAttribute("form", "unqualified"),
                                complexType));
                        currentPath = $"{currentPath}/{sf.RefObject}";
                    }

                    var nsf = sf as NodeDataBlock;
                    if (nsf != null)
                    {
                        complexType.Add(GetShemaForNode(nsf));
                        sf.XmlPath = $"{currentPath}/@{nsf.NodeType}";
                    }

                    var vsf = sf as VariableDataBlock;
                    if (vsf != null)
                    {
                        sequence.Add(GetShemaForVariable(vsf));
                        sf.XmlPath = $"{currentPath}/{vsf.VariableMetamodel.Name}";
                    }
                }
                lastElement = sequence;
            }
            return schema.ToString();
        }

        private static XElement GetShemaForNode(NodeDataBlock sf)
        {
            return new XElement(ns + "attribute",
                new XAttribute("name", sf.NodeType.ToString()),
                new XAttribute("type", sf.NodeType == NodeType.Since || sf.NodeType == NodeType.Till ? "xs:dateTime" : "xs:string"),
                new XAttribute("use", sf.NodeType == NodeType.UniqueName ? "required" : "optional"),
                new XAttribute("form", "unqualified"));
        }

        private static XElement GetShemaForVariable(VariableDataBlock sf)
        {
            return new XElement(ns + "element",
                new XAttribute("name", sf.VariableMetamodel.Name),
                new XAttribute("type", "xs:string"),
                new XAttribute("minOccurs", "1"),
                new XAttribute("maxOccurs", "1"),
                new XAttribute("nillable", "true"),
                new XAttribute("form", "unqualified"));
        }

    }
}
