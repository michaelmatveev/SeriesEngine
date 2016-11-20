namespace SeriesEngine.Msk1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    // элементы TreeNetwork
    [Table("pwk1.MainHierarchy")]
    public partial class MainHierarchy : NetworkTreeNode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MainHierarchy()
        {
            MainHierarchy1 = new HashSet<MainHierarchy>();
        }

        public int Id { get; set; }

        public int NetId { get; set; }

        public int? Region_Id { get; set; }

        public int? Consumer_Id { get; set; }

        public int? Contract_Id { get; set; }

        public int? ConsumerObject_Id { get; set; }

        public int? Point_Id { get; set; }

        public int? ReplaceId { get; set; }

        public int? Tag { get; set; }

        public virtual Network Network { get; set; }

        public virtual ConsumerObject ConsumerObject { get; set; }

        public virtual Consumer Consumer { get; set; }

        public virtual Contract Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainHierarchy> MainHierarchy1 { get; set; }

        public virtual MainHierarchy MainHierarchy2 { get; set; }

        public virtual Point Point { get; set; }

        public virtual Region Region { get; set; }

        public override NetworkTreeNode Parent
        {
            get
            {
                return (NetworkTreeNode)MainHierarchy2;
            }
        }

        public override NamedObject LinkedObject
        {
            get
            {
                if (Region != null) return Region;
                if (Consumer != null) return Consumer;
                if (Contract != null) return Contract;
                if (ConsumerObject != null) return ConsumerObject;
                if (Point != null) return Point;

                return null; 
            }
        }
    }
}
