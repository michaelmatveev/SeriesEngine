﻿using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Views;
using SeriesEngine.Core.DataAccess;

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

        //private Excel.Workbook _workbookToClose; 

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Application.WorkbookOpen += (w) => CreateWorkbookController(w);            
            ((Excel.AppEvents_Event)Application).NewWorkbook += (w) =>  CreateWorkbookController(w);
            //Application.WorkbookBeforeClose += (Excel.Workbook wb, ref bool c) =>
            //{
            //    _workbookToClose = wb;
            //};
            Application.WorkbookBeforeSave += (Excel.Workbook wb, bool save, ref bool cancel) => ApplicationControllers[wb].PreserveDataBlocks();
            Application.WorkbookActivate += (wb) =>
            {
                foreach (var c in ApplicationControllers.Values)
                {
                    if (c != wb)
                    {
                        c.StopGettingEventsFromRibbon();
                    }
                }
                var controller = ApplicationControllers[wb];
                controller.Activate();
                SetCaption(wb, controller.CurrentSolution);
            };
            Application.WorkbookDeactivate += (wb) =>
            {
                ApplicationControllers[wb].Deactivate();
            };
            //Application.WorkbookDeactivate += (wb) =>
            //{
            //    if(_workbookToClose == wb)
            //    {
            //        ApplicationControllers[wb].Deactivate();
            //        ApplicationControllers.Remove(wb);
            //    }
            //};
            
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
            ApplicationControllers.Add(wb, controller);
            controller.PropertyChanged += (s, e) => SetCaption(wb, controller.CurrentSolution); 
            controller.Configure();
        }

        private void SetCaption(Excel.Workbook wb, Solution s)
        {
            Application.ActiveWindow.Caption = s == null ? $"{wb.Name}" : $"{wb.Name} ({s.Name})";
        }

    }
}