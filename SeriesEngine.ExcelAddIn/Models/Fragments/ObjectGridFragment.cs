using SeriesEngine.ExcelAddIn.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Models.Fragments
{
    public class SubFragment
    {
        public string XmlPath { get; set; }
        public string Caption { get; set; }
        public int Level { get; set; }
        public string CollectionName { get; set; }
        public string RefObject { get; set; }
    }

    public enum NodeType
    {
        UniqueName,
        Since,
        Till,
        Path
    }

    public class NodeSubFragment : SubFragment
    {
        public NodeType NodeType;
    }

    public class VariableSubFragment : SubFragment
    {
        public Kind Kind;
        public string VariableName;
    }

    [Serializable]
    public class ObjectGridFragment : SheetFragment
    {

        public ObjectGridFragment() : base(null, new Period())
        {
        }

        public ObjectGridFragment(CollectionFragment parent, Period defaultPeriod) : base(parent, defaultPeriod)
        {
        }

        private static XNamespace ns = "http://www.w3.org/2001/XMLSchema";

        public string GetSchema()
        {
            
            var schema = new XDocument(
                new XElement(ns + "schema",
                    new XElement(ns + "element", new XAttribute("name", "DataToImport"), new XAttribute("nillable", "true"),
                        new XElement(ns + "complexType",
                            new XElement(ns + "sequence", new XAttribute("minOccurs", "0"))))));

            schema.Root.SetAttributeValue(XNamespace.Xmlns + "xs", ns);
            var lastElement = schema.Descendants().Where(d => !d.HasElements).Single();
            var currentPath = "/DataToImport"; 

            foreach (var sfGroup in SubFragments.GroupBy(sf => sf.Level).OrderBy(sfg => sfg.Key))
            {
                var complexType = new XElement(ns + "complexType");
                var sequence = new XElement(ns + "sequence", new XAttribute("minOccurs", "0"));
                complexType.Add(sequence);

                foreach (var sf in sfGroup) 
                {
                    if (!lastElement.Descendants(ns + "element").Any(d => d.Attribute("name").Value == sf.RefObject))
                    {
                        //minOccurs="0" maxOccurs="unbounded" nillable="true" form="unqualified"
                        lastElement.Add(
                            new XElement(ns + "element", new XAttribute("name", sf.RefObject), new XAttribute("minOccurs", "0"), new XAttribute("maxOccurs", "unbounded"), new XAttribute("nillable", "true"), new XAttribute("form", "unqualified"),
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
            return new XElement(ns + "attribute", new XAttribute("name", sf.NodeType.ToString()), new XAttribute("type", "xs:string"), new XAttribute("use", sf.NodeType == NodeType.UniqueName ? "required" : "optional"), new XAttribute("form", "unqualified"));
        }

        private XElement GetShemaForVariable(VariableSubFragment sf)
        {
            return new XElement(ns + "element", new XAttribute("name", sf.VariableName), new XAttribute("type", "xs:string"), new XAttribute("minOccurs", "1"), new XAttribute("maxOccurs", "1"), new XAttribute("nillable", "true"), new XAttribute("form", "unqualified"));
        }

        public string GetXml()
        {
            return Resources.TestGridData;
        }

        public IList<SubFragment> SubFragments = new List<SubFragment>();
        //{

            //yield return new SubFragment()
            //{
            //    Caption = "ISBN",
            //    XmlPath = "/BookInfo/Book/ISBN"
            //};

            //yield return new SubFragment()
            //{
            //    Caption = "Заголовок",
            //    XmlPath = "/BookInfo/Book/Title"
            //};

            //yield return new SubFragment()
            //{
            //    Caption = "Автор",
            //    XmlPath = "/BookInfo/Book/Author"
            //};

            //yield return new SubFragment()
            //{
            //    Caption = "Количество",
            //    XmlPath = "/BookInfo/Book/Quantity"
            //};

        //}

    }
}
