using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesEngine.Core.Helpers;

namespace SeriesEngine.Msk1
{
    public class MainHierarchyNetwork : Network
    {
        public MainHierarchyNetwork()
        {
            Nodes = new HashSet<MainHierarchyNode>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainHierarchyNode> Nodes { get; set; }

        public override ICollection<NetworkTreeNode> MyNodes
        {
            get
            {
                return Nodes.CastCollection<MainHierarchyNode, NetworkTreeNode>();
            }
        }
    }
}
