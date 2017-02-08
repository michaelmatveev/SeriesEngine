using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public abstract class BaseDataExporter
    {
        protected void ExportFromDataBlocks(int solutionId, IEnumerable<SheetDataBlock> dataBlocks)
        {
            foreach (var db in dataBlocks)
            {
                db.Export(solutionId, this);
            }
        }

        public abstract void ExportDataBlock(int solutionId, CollectionDataBlock fragment);

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
