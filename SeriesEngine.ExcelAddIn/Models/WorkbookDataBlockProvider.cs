using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using Microsoft.Office.Tools.Excel;
using FluentDateTime;
using Microsoft.Office.Core;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class WorkbookDataBlockProvider : IDataBlockProvider
    {
        private const string XmlNamespace = "http://www.seriesengine.com/SeriesEngine.ExcelAddIn/GridFragments";
        private readonly Workbook _workbook;

        public WorkbookDataBlockProvider(Workbook workbook)
        {
            _workbook = workbook;
        }

        public void AddDataBlock(CollectionDataBlock fragment)
        {
            var ns = XNamespace.Get(XmlNamespace);
            //var doc = new XDocument(
            //    new XElement(ns + "ObjectGrid",
            //        new XAttribute(ns + "Version", "1"),
            //        new XAttribute(ns + "Name", fragment.Name),
            //        new XAttribute(ns + "Sheet", fragment.Sheet),
            //        new XAttribute(ns + "Cell", fragment.Cell)));
            var doc = new XDocument(
                new XElement(ns + "ObjectGrid",
                    new XAttribute("Version", "1"),
                    new XAttribute("Name", fragment.Name),
                    new XAttribute("Sheet", fragment.Sheet),
                    new XAttribute("Cell", fragment.Cell)));

            _workbook.CustomXMLParts.Add(doc.ToString());            
        }

        public DataBlock CopyDataBlock(DataBlock sourceFragment, CollectionDataBlock sourceCollection)
        {
            throw new NotImplementedException();
        }

        public DataBlock CreateDataBlock(CollectionDataBlock source)
        {
            throw new NotImplementedException();            
        }

        public void DeleteDataBlock(DataBlock fragment)
        {
            throw new NotImplementedException();
        }

        private const string CustomPropertyPeriodId = "sePeriodId";
        private static XmlSerializer PeriodSerializer = new XmlSerializer(typeof(Period));

        public Period GetDefaultPeriod()
        {
            var properties = (DocumentProperties)_workbook.CustomDocumentProperties;
            var property = properties.FindPropertyByName(CustomPropertyPeriodId);

            if (property == null)
            {
                SetDefaultPeriod(Period.Default);
                return Period.Default;
            }
            else
            {
                var part = _workbook.CustomXMLParts.SelectByID(property.Value);
                using (var reader = new StringReader(part.DocumentElement.XML))
                {
                    return (Period)PeriodSerializer.Deserialize(reader);
                }
            }
        }

        public void SetDefaultPeriod(Period period)
        {
            var properties = (DocumentProperties)_workbook.CustomDocumentProperties;
            var property = properties.FindPropertyByName(CustomPropertyPeriodId);

            using (var writer = new StringWriter())
            {
                PeriodSerializer.Serialize(writer, period);

                var partId = property?.Value as string;
                if (partId != null)
                {
                    _workbook.CustomXMLParts.SelectByID(partId).Delete();
                }

                partId = _workbook.CustomXMLParts.Add(writer.ToString()).Id;
                properties.CreateOrUpdateStringProperty(CustomPropertyPeriodId, partId);
            }
        }

        public IEnumerable<BaseDataBlock> GetDataBlocks(string filter)
        {
            var result = new List<BaseDataBlock>();
            var gridParts = _workbook.CustomXMLParts
                .Cast<CustomXMLPart>()
                .Where(p => !p.BuiltIn && p.NamespaceURI == XmlNamespace);
            var defaultPeriod = GetDefaultPeriod();
            foreach (var part in gridParts)
            {
                var doc = XDocument.Parse(part.XML);
                result.Add(XmlToDataBlockConverter.GetDataBlock(doc, defaultPeriod));
            };
             
            return result;
        }

    }
}
