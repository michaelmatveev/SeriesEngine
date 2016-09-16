using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Presenters;

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
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void CreateWorkbookController(Excel.Workbook wb)
        {
            var controller = new ExcelApplicationController
            {
                PaneCollection = CustomTaskPanes,
                MainRibbon = Globals.Ribbons.Ribbon,
                CurrentDocument = Globals.Factory.GetVstoObject(wb)
            };
            controller.Configure();
            ApplicationControllers.Add(wb, controller);
            controller.GetInstance<MainMenuPresenter>().Run();
        }

    }
}
