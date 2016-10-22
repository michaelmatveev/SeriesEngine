using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.ExcelAddIn.Models.Fragments;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class DataExporter : IDataExporter
    {
        private Workbook _workbook;
        private Random _random = new Random();

        public DataExporter(Workbook workbook)
        {
            _workbook = workbook;
        }

        public void ExportFromFragments(IEnumerable<SheetFragment> fragments)
        {
            foreach (var f in fragments)
            {
                if (f is NodeFragment)
                {
                    ExportNodeFragment((NodeFragment)f);
                }
            }
        }

        private void ExportNodeFragment(NodeFragment fragment)
        {
            Excel.Worksheet sheet = _workbook.Sheets[fragment.Sheet];

            MockNetworkProvider netwrokProvider = new MockNetworkProvider();
            var network = netwrokProvider.GetNetworks(string.Empty).FirstOrDefault() as NetworkTree;
            int i = 1;
            var objectName = sheet.get_Range(fragment.Cell).Offset[i, 0].Value2;
            while(objectName != null)
            {
                if (!network.Nodes
                    .Where(n => n.LinkedObject.ObjectModel == fragment.Model)
                    .Any(n => n.NodeName == objectName))
                {
                    MockNetworkProvider.mainTree.Nodes.Add(new NetworkTreeNode
                    {
                        Parent = null,
                        LinkedObject = new ManagedObject()
                        {
                            Name = objectName,
                            ObjectModel = fragment.Model
                        }
                    });
                }
                objectName = sheet.get_Range(fragment.Cell).Offset[i++, 0].Value2;
            }
        }

    }
}
