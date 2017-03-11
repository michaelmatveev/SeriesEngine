using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace SeriesEngine.ExcelAddIn.Helpers
{
    public static class WorkbookExtensions
    {
        private const string IdPropertyName = "SeriesEngineId";
        public static string GetId(this Excel.Workbook wb)
        {
            DocumentProperties properties = null;
            DocumentProperty property = null;
            string result = string.Empty;
            try
            {
                properties = (DocumentProperties)wb.CustomDocumentProperties;
                for (int i = 1; i <= properties.Count; i++)
                {
                    property = properties[i];
                    if (property.Name == IdPropertyName)
                    {
                        result = property.Value.ToString();
                    }
                    if (property != null)
                    {
                        Marshal.ReleaseComObject(property);
                    }
                }
                if (string.IsNullOrEmpty(result))
                {
                    result = Guid.NewGuid().ToString();
                    properties.Add(IdPropertyName, false, MsoDocProperties.msoPropertyTypeString, result);
                }

                return result;
            }
            finally
            {
                if (properties != null)
                {
                    Marshal.ReleaseComObject(properties);
                }
            }
        }

    }
}
