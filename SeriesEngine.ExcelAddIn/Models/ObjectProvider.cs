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

        public void ChangeData(int networkId, IEnumerable<IStateObject> objectsToChange)
        {
            var network = _networksProvider.GetNetworkById(networkId);
            network.UpdateVariables(objectsToChange);
        }

        public EditorObject GetSelectedObject(CurrentSelection selection, Solution solution)
        {
            var sheet = _workbook.ActiveSheet as Excel.Worksheet;
            var excelSelection = selection as ExcelCurrentSelection; 

            var column = sheet
                .ListObjects
                .Cast<Excel.ListObject>()
                .SelectMany(l => l.ListColumns.Cast<Excel.ListColumn>().Where(lc => SelectionInRange(lc.DataBodyRange, excelSelection.Row, excelSelection.Column)))
                .SingleOrDefault();

            var listObject = column.Parent as Excel.ListObject;
            if(column!= null && column.XPath.Value.EndsWith("@UniqueName"))
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

                return new EditorObject
                {
                    Name = selection.Value,
                    NetworkId = network.Id,
                    NodeId = id == null ? -1 : int.Parse(id.Value)
                };
            }

            return null;  
        }

        public EditPeriodVariables GetSelectedPeriodVaraible(CurrentSelection selection, Solution solution)
        {
            var sheet = _workbook.ActiveSheet as Excel.Worksheet;
            var excelSelection = selection as ExcelCurrentSelection;

            var column = sheet
                .ListObjects
                .Cast<Excel.ListObject>()
                .SelectMany(l => l.ListColumns.Cast<Excel.ListColumn>().Where(lc => SelectionInRange(lc.DataBodyRange, excelSelection.Row, excelSelection.Column)))
                .SingleOrDefault();

            var listObject = column.Parent as Excel.ListObject;
            if (column != null)
            {
                var collectionDatablock = _blockProvider
                    .GetDataBlocks()
                    .OfType<CollectionDataBlock>()
                    .SingleOrDefault(db => db.Name == listObject.Name);

                var period = collectionDatablock.PeriodType == PeriodType.Common ? _blockProvider.GetDefaultPeriod() : collectionDatablock.CustomPeriod;

                var path = column.XPath.Value.Split(new[] { '/' });
                var variableName = path.Last();
                var objTypeName = path.ElementAt(path.Length - 2);

                var variableBlock = collectionDatablock
                    .DataBlocks
                    .OfType<VariableDataBlock>()
                    .Where(db => db.VariableMetamodel.Name == variableName)
                    .SingleOrDefault();

                if (variableBlock != null)
                {
                    var network = _networksProvider
                        .GetNetwork(solution.Id, collectionDatablock.NetworkName, new[] { variableBlock }, period);

                    var parentPath = $"{string.Join("/", path.Take(path.Length - 1))}/@UniqueName";
                    var columnWithObjectName = listObject
                        .ListColumns
                        .OfType<Excel.ListColumn>()
                        .Where(c => c.XPath.Value == parentPath)
                        .SingleOrDefault();

                    var objName = sheet.Cells[excelSelection.Row, listObject.DataBodyRange.Column + columnWithObjectName.Index - 1].Value;

                    NamedObject obj = network.FindObject(objTypeName, objName);                    
                    IEnumerable<PeriodVariable> variableValues = obj.GetPeriodVariable(variableBlock.VariableMetamodel, period);

                    var result = new EditPeriodVariables
                    {
                        NetworkId = network.Id,
                        Object = obj,
                        VariableMetamodel = variableBlock.VariableMetamodel,
                        SelectedPeriod = period
                    };
                    result.ValuesForPeriod.AddRange(variableValues);
                    return result;
                }
            }

            return null;
        }

        public void RenameObject(EditorObject objectToRename)
        {
            var network = _networksProvider.GetNetworkById(objectToRename.NetworkId);
            network.RenameObjectLinkedWithNode(objectToRename.NodeId, objectToRename.Name);
        }

        public void DeleteObject(EditorObject objectToDelete)
        {
            var network = _networksProvider.GetNetworkById(objectToDelete.NetworkId);
            network.DeleteObjectLinkedWithNode(objectToDelete.NodeId);
        }

        //public void UpdatePeriodVaraible(EditPeriodVariables variables)
        //{
        //    var network = _networksProvider.GetNetworkById(variables.NetworkId);
        //    network.UpdateVariables(variables.ValuesForPeriod);
        //}

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
