using System;

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
