using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class NetworkTree : Network
    {
        public List<NetworkTreeNode> Nodes { get; } = new List<NetworkTreeNode>();
    }
}
