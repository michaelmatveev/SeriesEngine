using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Properties;
using SeriesEngine.App.EventData;

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
            Application.WorkbookBeforeClose += (Excel.Workbook wb, ref bool c) => ApplicationControllers.Remove(wb);
            Application.WorkbookActivate += (wb) => ApplicationControllers[wb].Raise(new RestoreMenuStateEventData());
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void CreateWorkbookController(Excel.Workbook wb)
        {
            var controller = new ExcelApplicationController
            {
                //IsActive = true,
                PaneCollection = CustomTaskPanes,
                MainRibbon = Globals.Ribbons.Ribbon,
                CurrentDocument = Globals.Factory.GetVstoObject(wb)
            };
            controller.Configure();
            ApplicationControllers.Add(wb, controller);
            controller.Raise(new InitializeEventData());

            AddTestGrid(wb);//TODO remove this code
        }

        private void AddTestGrid(Excel.Workbook wb)
        {
            var part = wb.CustomXMLParts.Add(Resources.TestGrid);
        }

    }
}
