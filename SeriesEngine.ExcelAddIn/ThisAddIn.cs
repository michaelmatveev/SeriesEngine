using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.Core.DataAccess;
using Microsoft.Office.Core;

namespace SeriesEngine.ExcelAddIn
{
    public partial class ThisAddIn
    {
        private static Dictionary<string, ExcelApplicationController> ApplicationControllers 
            = new Dictionary<string, ExcelApplicationController>();

        private void InternalStartup()
        {
            Startup += (s, e) => ThisAddIn_Startup(s, e);
            Shutdown += (s, e) => ThisAddIn_Shutdown(s, e);
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Application.WorkbookOpen += (wb) => CreateWorkbookController(wb);            
            ((Excel.AppEvents_Event)Application).NewWorkbook += (wb) => CreateWorkbookController(wb);

            Application.WorkbookBeforeSave += (Excel.Workbook wb, bool save, ref bool cancel) =>
            {
                var wbId = GetWorkbookId(wb);
                ApplicationControllers[wbId].PreserveDataBlocks();
            };

            Application.WorkbookAfterSave += (wb, b) =>
            {
                var wbId = GetWorkbookId(wb);
                var currentSolution = ApplicationControllers[wbId].CurrentSolution;
                SetCaption(wb, currentSolution);
            };

            Application.WorkbookActivate += (wb) =>
            {
                foreach (var c in ApplicationControllers.Values)
                {
                    c.StopGettingEventsFromRibbon();
                }
                var wbId = GetWorkbookId(wb);
                var controller = ApplicationControllers[wbId];
                controller.Activate();
                SetCaption(wb, controller.CurrentSolution);
            };

            Application.WorkbookDeactivate += (wb) =>
            {
                var wbId = GetWorkbookId(wb);
                ApplicationControllers[wbId].Deactivate();
            };          
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void CreateWorkbookController(Excel.Workbook wb)
        {
            foreach (var c in ApplicationControllers.Values)
            {
                c.StopGettingEventsFromRibbon();
            }

            var controller = new ExcelApplicationController
            {
                PaneCollection = CustomTaskPanes,
                MainRibbon = new RibbonWrapper(Globals.Ribbons.Ribbon),
                CurrentDocument = Globals.Factory.GetVstoObject(wb)
            };

            var wbId = GetWorkbookId(wb);
            ApplicationControllers.Add(wbId, controller);
            controller.PropertyChanged += (s, e) => SetCaption(wb, controller.CurrentSolution); 
            controller.Configure();
        }

        private void SetCaption(Excel.Workbook wb, Solution s)
        {
            Application.ActiveWindow.Caption = s == null ? $"{wb.Name}" : $"{wb.Name} ({s.Name})";
        }

        private const string IdPropertyName = "SeriesEngineId";
        private static string GetWorkbookId(Excel.Workbook wb)
        {            
            var properties = (DocumentProperties)wb.CustomDocumentProperties;
            foreach (DocumentProperty prop in properties)
            {
                if (prop.Name == IdPropertyName)
                {
                    return prop.Value.ToString();
                }
            }

            var id = Guid.NewGuid();
            properties.Add(IdPropertyName, false, MsoDocProperties.msoPropertyTypeString, id.ToString());
            return id.ToString();
        }

        private bool IsWorkbookOpen(Excel.Workbook wb)
        {
            try
            {
                Application.Workbooks.get_Item(wb);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}