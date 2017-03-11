using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.Core.DataAccess;
using System.Windows.Forms;
using SeriesEngine.ExcelAddIn.Helpers;

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
                using (SafeCom.ReleaseAfterUsing(wb))
                {
                    var wbId = wb.GetId();
                    ApplicationControllers[wbId].PreserveDataBlocks();
                }
            };

            Application.WorkbookAfterSave += (wb, b) =>
            {
                using (SafeCom.ReleaseAfterUsing(wb))
                {
                    var wbId = wb.GetId();
                    var currentSolution = ApplicationControllers[wbId].CurrentSolution;
                    SetCaption(wb.Name, currentSolution);
                }
            };

            Application.WorkbookActivate += (wb) =>
            {
                using (SafeCom.ReleaseAfterUsing(wb))
                {
                    foreach (var c in ApplicationControllers.Values)
                    {
                        c.StopGettingEventsFromRibbon();
                    }
                    var wbId = wb.GetId();
                    var controller = ApplicationControllers[wbId];
                    controller.Activate();
                    SetCaption(wb.Name, controller.CurrentSolution);
                }
            };

            Application.WorkbookDeactivate += (wb) =>
            {
                using (SafeCom.ReleaseAfterUsing(wb))
                {
                    var wbId = wb.GetId();
                    ApplicationControllers[wbId].Deactivate();
                }
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

            using (SafeCom.ReleaseAfterUsing(wb))
            {
                var wbId = wb.GetId();
                var controller = new ExcelApplicationController(
                    () => Globals.Factory.GetVstoObject(FindWorkbookById(wbId)),
                    new RibbonWrapper(Globals.Ribbons.Ribbon),
                    CustomTaskPanes);

                ApplicationControllers.Add(wbId, controller);
                var wbName = wb.Name;
                controller.PropertyChanged += (s, e) => SetCaption(wbName, controller.CurrentSolution);
                controller.Configure();
            }
        }

        private void SetCaption(string workbookName, Solution s)
        {
            Application.ActiveWindow.Caption = s == null ? $"{workbookName}" : $"{workbookName} ({s.Name})";
        }

        private Excel.Workbook FindWorkbookById(string id)
        {
            foreach (Excel.Workbook wb in Application.Workbooks)
            {
                if (id == wb.GetId())
                {
                    return wb;
                }
            }
            return null;
        }

    }
}