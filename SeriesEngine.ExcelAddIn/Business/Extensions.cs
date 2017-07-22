using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public static class Extensions
    {
        public static DocumentProperty FindPropertyByName(this DocumentProperties properties, string propertyName)
        {
            return properties.Cast<DocumentProperty>().SingleOrDefault(p => p.Name == propertyName);
        }

        public static void CreateOrUpdateStringProperty(this DocumentProperties properties, string propertyName, string value)
        {
            var property = properties.FindPropertyByName(propertyName);

            if (property == null)
            {
                properties.Add(propertyName, false, MsoDocProperties.msoPropertyTypeString, value);
            }
            else
            {
                property.Value = value;
            }
        }

    }
}
