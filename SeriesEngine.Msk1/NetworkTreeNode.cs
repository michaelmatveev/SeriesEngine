using SeriesEngine.Core.DataAccess;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesEngine.Msk1
{
    public abstract class NetworkTreeNode:
        IStateObject
    {
        [NotMapped]
        public ObjectState State { get; set; }
        
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

        public void SetLinkedObject(NamedObject obj)
        {
            var property = this.GetType().GetProperty(obj.ObjectModel.Name);
            property.SetValue(this, obj);
        }

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTill { get; set; }

        [NotMapped]
        public bool IsMarkedFlag { get; set; }

    }
}
