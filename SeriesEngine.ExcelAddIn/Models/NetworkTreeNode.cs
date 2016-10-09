using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.ExcelAddIn.Models
{
    public class NetworkTreeNode
    {
        public string NodeName
        {
            get
            {
                return LinkedObject.Name;
            }
        }

        public NetworkTreeNode Parent { get; set; }
        public ManagedObject LinkedObject { get; set; }
        public DateTime Since { get; set; }
        public DateTime Till { get; set; }
    }
}
