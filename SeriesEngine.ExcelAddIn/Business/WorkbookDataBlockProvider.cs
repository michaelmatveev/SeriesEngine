﻿using System;
using System.Collections.Generic;
using System.Linq;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Core;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using SeriesEngine.Core.DataAccess;
using System.Xml.XPath;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class WorkbookDataBlockProvider : IDataBlockProvider
    {
        private readonly Workbook _workbook;
        private readonly List<BaseDataBlock> _dataBlocks;

        public WorkbookDataBlockProvider(Workbook workbook)
        {
            _workbook = workbook;
            _dataBlocks = new List<BaseDataBlock>();

            LoadDatablocks();
        }

        private void LoadDatablocks()
        {
            var gridParts = _workbook.CustomXMLParts
                .Cast<CustomXMLPart>()
                .Where(p => !p.BuiltIn && p.NamespaceURI == Constants.XmlNamespaceDataBlocks);
            _dataBlocks.Clear();
            var defaultPeriod = GetDefaultPeriod();
            foreach (var part in gridParts)
            {
                var doc = XDocument.Parse(part.XML);
                _dataBlocks.Add(DataBlockConverter.GetDataBlock(doc, defaultPeriod));
            };
        }

        public IEnumerable<BaseDataBlock> GetDataBlocks()
        {
            return _dataBlocks;
        }

        public string GetXml(string name)
        {
            var gridParts = _workbook.CustomXMLParts
                .Cast<CustomXMLPart>()
                .Where(p => !p.BuiltIn && p.NamespaceURI == Constants.XmlNamespaceDataBlocks)
                .ToList();

            foreach (var part in gridParts)
            {
                var doc = XDocument.Parse(part.XML);
                var partName = doc.Root.Attribute("Name")?.Value;
                if (partName == name)
                {
                    return doc.ToString();
                }
            };

            return string.Empty;

        }
        public void UpdateXml(string name, string xml)
        {
            var gridParts = _workbook.CustomXMLParts
                .Cast<CustomXMLPart>()
                .Where(p => !p.BuiltIn && p.NamespaceURI == Constants.XmlNamespaceDataBlocks)
                .ToList();

            CustomXMLPart target = null;
            foreach (var part in gridParts)
            {
                var doc = XDocument.Parse(part.XML);
                var partName = doc.Root.Attribute("Name")?.Value;
                if (partName == name)
                {
                    target = part;
                }
            };
            if (target != null)
            {
                target.Delete();
            }
            _workbook.CustomXMLParts.Add(xml);
            LoadDatablocks();
        }

        public void Save()
        {
            var gridParts = _workbook.CustomXMLParts
                .Cast<CustomXMLPart>()
                .Where(p => !p.BuiltIn && p.NamespaceURI == Constants.XmlNamespaceDataBlocks)
                .ToList();

            foreach(var part in gridParts)
            {
                part.Delete();
            }

            foreach(var db in _dataBlocks)
            {
                _workbook.CustomXMLParts.Add(DataBlockConverter.GetXml(db).ToString());
            }
        }

        public void AddDataBlock(CollectionDataBlock collectionDataBlock)
        {
            if (!_dataBlocks.Contains(collectionDataBlock))
            {
                _dataBlocks.Add(collectionDataBlock);
            }
        }

        public DataBlock CopyDataBlock(DataBlock sourceFragment, CollectionDataBlock sourceCollection)
        {
            throw new NotImplementedException();
        }

        public DataBlock CreateDataBlock(CollectionDataBlock source)
        {
            throw new NotImplementedException();            
        }

        public void DeleteDataBlock(BaseDataBlock dataBlock)
        {
            _dataBlocks.Remove(dataBlock);
        }

        private const string CustomPropertyPeriodId = "sePeriodId";
        private static XmlSerializer PeriodSerializer = new XmlSerializer(typeof(Period));

        private const string CustomSolutionId = "seSolutionId";

        public Period GetDefaultPeriod(CollectionDataBlock datablock = null)
        {
            var properties = (DocumentProperties)_workbook.CustomDocumentProperties;
            var property = properties.FindPropertyByName(CustomPropertyPeriodId);
            Period result;
            if (property == null)
            {
                SetDefaultPeriod(Period.Default);
                result = Period.Default;
            }
            else
            {
                var part = _workbook.CustomXMLParts.SelectByID(property.Value);
                using (var reader = new StringReader(part.DocumentElement.XML))
                {
                    result = (Period)PeriodSerializer.Deserialize(reader);
                }
            }

            if (datablock == null || datablock.PeriodType == PeriodType.Common)
            {
                return result;
            }
            else
            {
                if (datablock.PeriodType == PeriodType.Custom)
                {
                    return datablock.CustomPeriod;
                }
            }

            return null;
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

        public int GetLastSolutionId()
        {
            var properties = (DocumentProperties)_workbook.CustomDocumentProperties;
            var property = properties.FindPropertyByName(CustomSolutionId);

            if (property == null)
            {
                SetLastSolutionId(0);
                return 0;
            }
            else
            {
                return Int32.Parse(property.Value);
            }
        }

        public void SetLastSolutionId(int solutionId)
        {
            var properties = (DocumentProperties)_workbook.CustomDocumentProperties; 
            properties.CreateOrUpdateStringProperty(CustomSolutionId, solutionId.ToString());
        }

    }
}
