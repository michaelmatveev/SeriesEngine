using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class NetworkTreeNode
    {
        public string NodeName { get; set; }
        public NetworkTreeNode Parent { get; set; }
    }
}
