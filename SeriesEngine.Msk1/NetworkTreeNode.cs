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

        //public int NetId { get; set; }

        //public virtual Network Network { get; set; }
        [NotMapped]
        public abstract Network MyNetwork { get; set; }

        [NotMapped]
        public abstract NetworkTreeNode MyParent { get; set; }

        public string NodeName
        {
            get
            {
                return LinkedObject.GetName();
            }
        }

        public abstract NamedObject LinkedObject { get; }

        public void SetLinkedObject(NamedObject obj)
        {
            var property = this.GetType().GetProperty(obj.ObjectModel.Name);
            property.SetValue(this, obj);
        }

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTill { get; set; }
    }
}
