using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesEngine.Core.DataAccess
{
    public abstract class NetworkTreeNode :
        IStateObject
    {
        public const char PathSeparator = '|';

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NotMapped]
        public ObjectState State { get; set; }
        
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

        [NotMapped]
        public string Path
        {
            get
            {
                if(MyParent == null)
                {
                    return NodeName;
                }
                return $"{MyParent.Path}{PathSeparator}{NodeName}"; 
            }
        }

        public bool InPath(string path)
        {
            var pathElements = path.Split(PathSeparator);
            var myPathElements = Path.Split(PathSeparator);
            for(int i = 0; i < pathElements.Length; i++)
            {
                if(pathElements[i] != myPathElements[i])
                {
                    return false;
                }
                if((i + 1) == myPathElements.Length)
                {
                    break;
                }
            }
            return true;
        }

        [NotMapped]
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
