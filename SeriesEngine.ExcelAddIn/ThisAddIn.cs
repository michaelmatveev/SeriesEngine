﻿using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Properties;
using SeriesEngine.App.EventData;
using SeriesEngine.ExcelAddIn.Views;

namespace SeriesEngine.ExcelAddIn
{
    public partial class ThisAddIn
    {
        private static Dictionary<Excel.Workbook, ExcelApplicationController> ApplicationControllers 
            = new Dictionary<Excel.Workbook, ExcelApplicationController>();

        private void InternalStartup()
        {
            Startup += (s, e) => ThisAddIn_Startup(s, e);
            Shutdown += (s, e) => ThisAddIn_Shutdown(s, e);
        }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Application.WorkbookOpen += (w) => CreateWorkbookController(w);            
            ((Excel.AppEvents_Event)Application).NewWorkbook += (w) => CreateWorkbookController(w);
            Application.WorkbookBeforeClose += (Excel.Workbook wb, ref bool c) =>
            {
                ExcelApplicationController controller;
                if (ApplicationControllers.TryGetValue(wb, out controller))
                {
                    controller.ClosePane();
                    ApplicationControllers.Remove(wb);
                }
            };
            Application.WorkbookBeforeSave += (Excel.Workbook wb, bool saveAsUI, ref bool cancel) =>
            {
                ApplicationControllers[wb].PreserveDataBlocks();
            };
            Application.WorkbookActivate += (wb) =>
            {
                ExcelApplicationController controller;
                if (ApplicationControllers.TryGetValue(wb, out controller))
                {
                    controller.Activate();
                }
                else
                {
                    CreateWorkbookController(wb);
                }
            };
            Application.WorkbookDeactivate += (wb) =>
            {
                ExcelApplicationController controller;
                if (ApplicationControllers.TryGetValue(wb, out controller))
                {
                    controller.Deactivate();
                }
            };
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void CreateWorkbookController(Excel.Workbook wb)
        {
            var controller = new ExcelApplicationController
            {
                PaneCollection = CustomTaskPanes,
                MainRibbon = new RibbonWrapper(Globals.Ribbons.Ribbon),
                CurrentDocument = Globals.Factory.GetVstoObject(wb)
            };
            controller.Configure();
            ApplicationControllers.Add(wb, controller);
            controller.Raise(new InitializeEventData());
            //AddTestGrid(wb);//TODO remove this code
        }

        private void AddTestGrid(Excel.Workbook wb)
        {
            var part = wb.CustomXMLParts.Add(Resources.TestGrid);
        }

    }
}
