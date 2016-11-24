using System;

namespace SeriesEngine.Msk1
{
    public abstract class NetworkTreeNode
    {
        public int Id { get; set; }

        public string NodeName
        {
            get
            {
                return LinkedObject.GetName();
            }
        }

        public virtual MainHierarchyNode Parent { get; set; }
        public abstract NamedObject LinkedObject { get; }

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTill { get; set; }
    }
}
