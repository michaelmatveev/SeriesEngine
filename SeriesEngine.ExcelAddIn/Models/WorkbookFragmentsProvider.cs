using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models.Fragments;
using Microsoft.Office.Tools.Excel;
using FluentDateTime;
using Microsoft.Office.Core;
using System.IO;
using System.Xml.Serialization;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class WorkbookFragmentsProvider : IFragmentsProvider
    {
        private readonly Workbook _workbook;
        public WorkbookFragmentsProvider(Workbook workbook)
        {
            _workbook = workbook;
        }

        public void AddFragment(DataFragment fragment)
        {
            throw new NotImplementedException();
        }

        public DataFragment CopyFragment(DataFragment sourceFragment, CollectionFragment sourceCollection)
        {
            throw new NotImplementedException();
        }

        public DataFragment CreateFragment(CollectionFragment source)
        {
            throw new NotImplementedException();
        }

        public void DeleteFragment(DataFragment fragment)
        {
            throw new NotImplementedException();
        }

        //public string CustomPeriodId { get; set; };
        private const string CustomPropertyPeriodId = "sePeriodId";
        private static XmlSerializer PeriodSerializer = new XmlSerializer(typeof(Period));

        public Period GetDefaultPeriod()
        {
            var properties = (DocumentProperties)_workbook.CustomDocumentProperties;
            var property = properties.FindPropertyByName(CustomPropertyPeriodId);
            //if (!_workbook.DataHost.IsCached(CustomPeriodId, ""))
            //{
            //    this.StartCaching("dataSet1");
            //}

            //_workbook.DataHost.StartCaching(Cu)
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
               
        //private static XmlSerializer serializer = new XmlSerializer(typeof(Query));

        public IEnumerable<BaseFragment> GetFragments(string filter)
        {
            //var result = new List<BaseFragment>();
            //foreach(var part in _workbook.CustomXMLParts.Cast<CustomXMLPart>().Where(p => !p.BuiltIn))
            //{
            //    using (var reader = new StringReader(part.XML))
            //    {
            //        try
            //        {
            //            var grid = (ObjectGridFragment)serializer.Deserialize(reader);
            //            result.Add(grid);
            //        }
            //        catch
            //        {

            //        }
            //    }

            //};
            //return result;
            var defPeriod = GetDefaultPeriod();
            yield return new ObjectGridFragment(null, defPeriod)
            {
                Name = "customPart"
            };
        }



    }
}
