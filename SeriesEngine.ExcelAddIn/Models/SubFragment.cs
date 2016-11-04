using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class SubFragment
    {
        public string XmlPath { get; set; }
        public string Caption { get; set; }
        public int Level { get; set; }
        public string CollectionName { get; set; }
        public string RefObject { get; set; }
    }

    public enum NodeType
    {
        UniqueName,
        Since,
        Till,
        Path
    }

    public class NodeSubFragment : SubFragment
    {
        public NodeType NodeType;
    }

    public class VariableSubFragment : SubFragment
    {
        public Kind Kind;
        public string VariableName;
    }

}
