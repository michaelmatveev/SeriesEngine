using SeriesEngine.Core.DataAccess;
using SeriesEngine.ExcelAddIn.Models;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Business.Export
{
    public abstract class BaseDataExporter : IErrorAware
    {
        protected void ExportFromDataBlocks(Solution solution, IEnumerable<SheetDataBlock> dataBlocks)
        {
            foreach (var db in dataBlocks)
            {
                try
                {                                     
                    db.Export(solution, this);
                }
                catch(Exception ex)
                {
                    if (OnErrorOccured(ex.Message))
                    {
                        break;
                    }
                }
            }
        }

        public event EventHandler<ErrorOccuredEventArgs> ErrorOccured;
        protected bool OnErrorOccured(string message)
        {
            var args = new ErrorOccuredEventArgs
            {
                Message = message
            };
            ErrorOccured?.Invoke(this, args);
            return args.Cancel;
        }

        public abstract void ExportDataBlock(Solution solution, CollectionDataBlock fragment);

        //protected virtual void ExportGridFragment(ObjectGridFragment fragment)
        //{
        //    throw new NotImplementedException();
        //}

        //private void ExportNodeFragment(NodeFragment fragment)
        //{
        //    Excel.Worksheet sheet = _workbook.Sheets[fragment.Sheet];

        //    MockNetworkProvider netwrokProvider = new MockNetworkProvider();
        //    var network = netwrokProvider.GetNetworks(string.Empty).FirstOrDefault() as NetworkTree;
        //    int i = 1;
        //    var objectName = sheet.get_Range(fragment.Cell).Offset[i, 0].Value2;
        //    while(objectName != null)
        //    {
        //        if (!network.Nodes
        //            .Where(n => n.LinkedObject.ObjectModel == fragment.Model)
        //            .Any(n => n.NodeName == objectName))
        //        {
        //            MockNetworkProvider.mainTree.Nodes.Add(new NetworkTreeNode
        //            {
        //                Parent = null,
        //                LinkedObject = new ManagedObject()
        //                {
        //                    Name = objectName,
        //                    ObjectModel = fragment.Model
        //                }
        //            });
        //        }
        //        objectName = sheet.get_Range(fragment.Cell).Offset[i++, 0].Value2;
        //    }
        //}

    }

}
