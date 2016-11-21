namespace SeriesEngine.Msk1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pwk1.MainHierarchyNodes")]
    public partial class MainHierarchyNode : NetworkTreeNode
    {
        public int Id { get; set; }

        public int NetId { get; set; }

        public int? Region_Id { get; set; }

        public int? Consumer_Id { get; set; }

        public int? Contract_Id { get; set; }

        public int? ConsumerObject_Id { get; set; }

        public int? Point_Id { get; set; }

        public int? Tag { get; set; }

        public virtual Network Network { get; set; }

        public virtual ConsumerObject ConsumerObject { get; set; }

        public virtual Consumer Consumer { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual Point Point { get; set; }

        public virtual Region Region { get; set; }

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
