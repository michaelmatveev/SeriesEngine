using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using SeriesEngine.Core.DataAccess;
using System.Linq;
using SeriesEngine.App;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections.Generic;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class ObjectProvider : IObjectProvider
    {
        private readonly Workbook _workbook;
        private readonly IDataBlockProvider _blockProvider;
        private readonly INetworksProvider _networksProvider;

        public ObjectProvider(Workbook workbook, IDataBlockProvider blockProvider, INetworksProvider networksProvider)
        {
            _workbook = workbook;
            _blockProvider = blockProvider;
            _networksProvider = networksProvider;
        }

        public MyObject GetSelectedObject(CurrentSelection selection, Solution solution)
        {
            var sheet = _workbook.ActiveSheet as Excel.Worksheet;

            var column = sheet
                .ListObjects
                .Cast<Excel.ListObject>()
                .SelectMany(l => l.ListColumns.Cast<Excel.ListColumn>().Where(lc => SelectionInRange(lc.DataBodyRange, selection.Row, selection.Column)))
                .SingleOrDefault();

            var listObject = column.Parent as Excel.ListObject;
            if(column!= null)
            {
                var collectionDatablock = _blockProvider
                    .GetDataBlocks()
                    .OfType<CollectionDataBlock>()
                    .SingleOrDefault(db => db.Name == listObject.Name);

                var network = _networksProvider
                    .GetNetworks(solution.Id)
                    .SingleOrDefault(n => n.Name == collectionDatablock.NetworkName);

                var xml = collectionDatablock.GetXml(network, _blockProvider.GetDefaultPeriod());
                var xpath = GetXPathToNodeId(column.XPath.Value, selection.Value);
                var id = ((IEnumerable<object>)XDocument.Parse(xml).Root.XPathEvaluate(xpath))
                    .OfType<XAttribute>()
                    .FirstOrDefault();

                return new MyObject
                {
                    Name = selection.Value,
                    NetworkId = network.Id,
                    NodeId = id == null ? -1 : int.Parse(id.Value)
                };
            }

            return null;  
        }

        public void UpdateObject(MyObject objectToUpdate)
        {
            var network = _networksProvider.GetNetworkById(objectToUpdate.NetworkId);
            network.RenameObjectLinkedWithNode(objectToUpdate.NodeId, objectToUpdate.Name);
        }

        public void DeleteObject(CurrentSelection selection, Solution solution)
        {
            var objectToDelete = GetSelectedObject(selection, solution);
            var network = _networksProvider.GetNetworkById(objectToDelete.NetworkId);
            network.DeleteObjectLinkedWithNode(objectToDelete.NodeId);
        }

        private static bool SelectionInRange(Excel.Range range, int row, int column)
        {
            return column >= range.Column && column <= range.Column + range.Columns.Count - 1
                && row >= range.Row && row <= range.Row + range.Rows.Count - 1;
        }

        private static string GetXPathToNodeId(string nameXPath, string nameToFind)
        {
            var splitted = nameXPath.Split('/');
            var excludeUniqueName = splitted.Take(splitted.Length - 1);
            return $"{string.Join("/", excludeUniqueName)}[@UniqueName='{nameToFind}']/@NodeId";
        }
    }
}
