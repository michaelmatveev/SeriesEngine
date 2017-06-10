using Microsoft.Office.Tools.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using SeriesEngine.Core.DataAccess;
using System.Linq;
using SeriesEngine.App;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections.Generic;
using System;
using SeriesEngine.ExcelAddIn.Helpers;

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
            network.Update(objectsToChange);
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

                var network = _networksProvider.GetNetwork(solution, collectionDatablock.NetworkName);

                var xpath = GetXPathToNodeId(column.XPath.Value, selection.Value.ToString());
                var id = ((IEnumerable<object>)collectionDatablock.Xml.Root.XPathEvaluate(xpath))
                    .OfType<XAttribute>()
                    .FirstOrDefault();

                return new EditorObject
                {
                    Name = selection.Value.ToString(),
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

                var path = column.XPath.Value.Split(new[] { '/' });
                var element = path.Last();
                var variableName = VariableNameParser.ExtractVariableName(element);
                var shift = VariableNameParser.ExtractPeriodShift(element);
                var period = _blockProvider.GetDefaultPeriod(collectionDatablock);
                period.From = period.From.AddMonths(shift);
                period.Till = period.Till.AddMonths(shift);

                var objTypeName = path.ElementAt(path.Length - 2);
                
                var variableBlock = collectionDatablock
                    .DataBlocks
                    .OfType<VariableDataBlock>()
                    .Where(db => db.VariableMetamodel.Name == variableName)
                    .FirstOrDefault();

                if (variableBlock != null)
                {
                    var networkTree = _networksProvider
                        //.GetNetwork(solution.Id, collectionDatablock.NetworkName, new[] { variableBlock }, period); 
                        .GetNetwork(solution, collectionDatablock.NetworkName, collectionDatablock.DataBlocks, period); //не оптимально запрашивать все данные

                    var parentPath = $"{string.Join("/", path.Take(path.Length - 1))}/@UniqueName";
                    var columnWithObjectName = listObject
                        .ListColumns
                        .OfType<Excel.ListColumn>()
                        .Where(c => c.XPath.Value == parentPath)
                        .SingleOrDefault();

                    var objName = sheet.Cells[excelSelection.Row, listObject.DataBodyRange.Column + columnWithObjectName.Index - 1].Value;

                    NamedObject obj = networkTree.FindObject(objTypeName, objName);                    
                    IEnumerable<PeriodVariable> variableValues = obj.GetPeriodVariable(variableBlock.VariableMetamodel);
                    
                    var result = new EditPeriodVariables
                    {
                        NetworkId = networkTree.Id,
                        Object = obj,
                        VariableMetamodel = variableBlock.VariableMetamodel,
                        SelectedPeriod = period,
                        InitialValue = variableValues.Where(pv => pv.Date < period.From).LastOrDefault()
                    };
                    
                    result.ValuesForPeriod.AddRange(variableValues.Where(pv => period.Include(pv.Date)));
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
