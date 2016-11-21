﻿using SeriesEngine.Msk1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Models.Fragments
{
    [Serializable]
    public class ObjectGridFragment : SheetFragment
    {
        public bool ShowHeader { get; set; } = true;
        
        public object Tag { get; set; }

        public ObjectGridFragment() : base(null, new Period())
        {
        }

        public ObjectGridFragment(CollectionFragment parent, Period defaultPeriod) : base(parent, defaultPeriod)
        {
        }

        private static XNamespace ns = "http://www.w3.org/2001/XMLSchema";
        private static XNamespace msdata = "urn:schemas-microsoft-com:xml-msdata";

        public string GetSchema()
        {            
            var schema = new XDocument(
                new XElement(ns +"schema",
                    new XElement(ns + "element", 
                        new XAttribute("name", NetworkTree.RootName), 
                        new XAttribute("nillable", "true"),
                        new XElement(ns + "complexType",
                            new XElement(ns + "sequence", new XAttribute("minOccurs", "0"))))));

            schema.Root.SetAttributeValue(XNamespace.Xmlns + "xs", ns);
            var lastElement = schema.Descendants().Where(d => !d.HasElements).Single();
            var currentPath = $"/{NetworkTree.RootName}"; 

            foreach (var sfGroup in SubFragments.GroupBy(sf => sf.Level).OrderBy(sfg => sfg.Key))
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

                    var nsf = sf as NodeSubFragment;
                    if (nsf != null)
                    {
                        complexType.Add(GetShemaForNode(nsf));
                        sf.XmlPath = $"{currentPath}/@{nsf.NodeType}";
                    }

                    var vsf = sf as VariableSubFragment;
                    if (vsf != null)
                    {
                        sequence.Add(GetShemaForVariable(vsf));
                        sf.XmlPath = $"{currentPath}/{vsf.VariableName}";                        
                    }
                }
                lastElement = sequence;                
            }
            return schema.ToString();
        }

        private XElement GetShemaForNode(NodeSubFragment sf)
        {
            return new XElement(ns + "attribute",
                new XAttribute("name", sf.NodeType.ToString()),
                new XAttribute("type", "xs:string"),
                new XAttribute("use", sf.NodeType == NodeType.UniqueName ? "required" : "optional"),
                new XAttribute("form", "unqualified"));//,
                //new XAttribute(msdata + "PrimaryKey", sf.NodeType == NodeType.UniqueName ? "true" : "false"));
        }

        private XElement GetShemaForVariable(VariableSubFragment sf)
        {
            return new XElement(ns + "element", 
                new XAttribute("name", sf.VariableName), 
                new XAttribute("type", "xs:string"), 
                new XAttribute("minOccurs", "1"), 
                new XAttribute("maxOccurs", "1"), 
                new XAttribute("nillable", "true"), 
                new XAttribute("form", "unqualified"));
        }

        public string GetXml(ICollection<Network> networks)
        {
            var network = networks.OfType<NetworkTree>().First();
            var xml = network.ConvertToXml(SubFragments);
            return xml.ToString();
        }

        public IList<SubFragment> SubFragments = new List<SubFragment>();

        public override void Export(BaseDataExporter exproter)
        {
            exproter.ExportFragment(this);
        }

        public override void Import(BaseDataImporter importer)
        {
            importer.ImportFragment(this);
        }
    }
}