using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models.Fragments
{

    [Serializable]
    public class ObjectGridFragment : SheetFragment
    {
        public class SubFragment
        {
            public string XmlPath { get; set; }
            public string Caption { get; set; }
        }

        public ObjectGridFragment() : base(null, new Period())
        {
        }

        public ObjectGridFragment(CollectionFragment parent, Period defaultPeriod) : base(parent, defaultPeriod)
        {
        }

        public string GetSchema()
        {
            return @"<xsd:schema xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><xsd:element nillable=""true"" name=""BookInfo""><xsd:complexType><xsd:sequence minOccurs=""0""><xsd:element minOccurs=""0"" maxOccurs=""unbounded"" nillable=""true"" name=""Book"" form=""unqualified""><xsd:complexType><xsd:sequence minOccurs=""0""><xsd:element minOccurs=""0"" nillable=""true"" type=""xsd:string"" name=""ISBN"" form=""unqualified""></xsd:element><xsd:element minOccurs=""0"" nillable=""true"" type=""xsd:string"" name=""Title"" form=""unqualified""></xsd:element><xsd:element minOccurs=""0"" nillable=""true"" type=""xsd:string"" name=""Author"" form=""unqualified""></xsd:element><xsd:element minOccurs=""0"" nillable=""true"" type=""xsd:integer"" name=""Quantity"" form=""unqualified""></xsd:element></xsd:sequence></xsd:complexType></xsd:element></xsd:sequence></xsd:complexType></xsd:element></xsd:schema>";
        }

        public string GetXml()
        {
            return "<?xml version='1.0'?><BookInfo><Book><ISBN>989-0-487-04641-2</ISBN><Title>My World</Title><Author>Nancy Davolio</Author><Quantity>121</Quantity></Book><Book><ISBN>981-0-776-05541-0</ISBN><Title>Get Connected</Title><Author>Janet Leverling</Author><Quantity>435</Quantity></Book><Book><ISBN>999-1-543-02345-2</ISBN><Title>Honesty</Title><Author>Robert Fuller</Author><Quantity>315</Quantity></Book></BookInfo>";
        } 

        public IEnumerable<SubFragment> SubFragments()
        {
            yield return new SubFragment()
            {
                Caption = "ISBN",
                XmlPath = "/BookInfo/Book/ISBN"
            };

            yield return new SubFragment()
            {
                Caption = "Заголовок",
                XmlPath = "/BookInfo/Book/Title"
            };

            yield return new SubFragment()
            {
                Caption = "Автор",
                XmlPath = "/BookInfo/Book/Author"
            };

            yield return new SubFragment()
            {
                Caption = "Количество",
                XmlPath = "/BookInfo/Book/Quantity"
            };

        }

    }
}
