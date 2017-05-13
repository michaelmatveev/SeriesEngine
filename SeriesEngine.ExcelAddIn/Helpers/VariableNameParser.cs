using SeriesEngine.Core;
using SeriesEngine.ExcelAddIn.Models.DataBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Helpers
{
    public class ParsedVariable
    {
        public Variable VariableModel { get; set; }
        public int Shift { get; set; }
    } 

    public class VariableNameParser
    {
        public static string GetVariableElementName(VariableDataBlock datablock)
        {
            var suffix = datablock.Shift != 0 ? "." + datablock.Shift.ToString() : string.Empty;
            return $"{datablock.VariableMetamodel.Name}{suffix}";
        }

        public static ParsedVariable GetVariableModel(ObjectMetamodel objectModel, string variableElementName)
        {
            var varName = Regex.Match(variableElementName, @"^\w*").Value;
            var shiftMatch = Regex.Match(variableElementName, @"(?=.)-?\d+$");
            return new ParsedVariable
            {
                VariableModel = objectModel.Variables.Single(v => v.Name == varName),
                Shift = shiftMatch.Success ? int.Parse(shiftMatch.Value) : 0
            };
        }
    }
}
