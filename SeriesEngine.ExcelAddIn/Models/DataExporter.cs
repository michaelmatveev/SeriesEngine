using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models.Fragments;
using SeriesEngine.App;
using SeriesEngine.App.CommandArgs;
using System.Diagnostics;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataExporter : ICommand<SaveAllCommandArgs>
    {
        private Workbook _workbook;
        private readonly IFragmentsProvider _fragmentsProvider;
        private readonly INetworksProvider _networksProvider;
        private Random _random = new Random();

        public DataExporter(Workbook workbook, IFragmentsProvider fragmentsProvider, INetworksProvider networksProvider)
        {
            _workbook = workbook;
            _fragmentsProvider = fragmentsProvider;
            _networksProvider = networksProvider;
        }

        public void Execute(SaveAllCommandArgs commandData)
        {
            var fragmentsToExport = _fragmentsProvider.GetFragments(string.Empty).OfType<SheetFragment>();
            ExportFromFragments(fragmentsToExport);
        }

        private void ExportFromFragments(IEnumerable<SheetFragment> fragments)
        {
            foreach(var f in fragments)
            {
                if (f is ObjectGridFragment)
                {
                    ExportGridFragment((ObjectGridFragment)f);
                }
            }
        }

        private void ExportGridFragment(ObjectGridFragment fragment)
        {
            var listObject = fragment.Tag as ListObject;
            foreach(Excel.Range row in listObject.DataBodyRange.Rows)
            {
                foreach (Excel.ListColumn column in listObject.ListColumns)
                {
                    var v = row[1, column.Index].Value;
                    Trace.WriteLine(v);
                }
            }
        }

        //public void ExportFromFragments(IEnumerable<SheetFragment> fragments)
        //{
        //    foreach (var f in fragments)
        //    {
        //        if (f is NodeFragment)
        //        {
        //            ExportNodeFragment((NodeFragment)f);
        //        }
        //    }
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
