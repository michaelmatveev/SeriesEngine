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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MainHierarchyNode()
        {
            Children = new HashSet<MainHierarchyNode>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainHierarchyNode> Children { get; set; }

        public int NetId { get; set; }

        public virtual Network Network { get; set; }

        public int? ParentId { get; set; }

        public int? Region_Id { get; set; }

        public int? Consumer_Id { get; set; }

        public int? Contract_Id { get; set; }

        public int? ConsumerObject_Id { get; set; }

        public int? Point_Id { get; set; }

        public int? Tag { get; set; }

        public virtual ConsumerObject ConsumerObject { get; set; }

        public virtual Consumer Consumer { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual Point Point { get; set; }

        public virtual Region Region { get; set; }

        public override NamedObject LinkedObject
        {
            get
            {
                if (Region != null)
                {
                    Region.ObjectModel = RegionModel;
                    return Region;
                }

                if (Consumer != null)
                {
                    Consumer.ObjectModel = ConsumerModel;
                    return Consumer;
                }
                if (Contract != null)
                {
                    Contract.ObjectModel = ContractModel;
                    return Contract;
                }
                if (ConsumerObject != null)
                {
                    ConsumerObject.ObjectModel = ConsumerObjectModel;
                    return ConsumerObject;
                }
                if (Point != null)
                {
                    Point.ObjectModel = PointModel;
                    return Point;
                }
                return null;
            }

        }

        public static ObjectMetamodel RegionModel = new ObjectMetamodel
        {
            Name = "Region",
        };

        public static ObjectMetamodel ConsumerModel = new ObjectMetamodel
        {
            Name = "Consumer",
        };

        public static ObjectMetamodel ContractModel = new ObjectMetamodel
        {
            Name = "Contract",
            Variables = new List<Variable>
            {
                new Variable
                {
                    Name = "Номер договора"
                },
                new Variable
                {
                    Name = "Дата договора"
                },
                new Variable
                {
                    Name = "Ценовая категория"
                },
            }
        };

        public static ObjectMetamodel ConsumerObjectModel = new ObjectMetamodel
        {
            Name = "ConsumerObject",
        };

        public static ObjectMetamodel PointModel = new ObjectMetamodel
        {
            Name = "Point",
            Variables = new List<Variable>
            {
                new Variable
                {
                    Name = "Наименование"
                },
                new Variable
                {
                    Name = "Уровень напряжения"
                },
                new Variable
                {
                    Name = "Максимальная мощность"
                },
                new Variable
                {
                    Name = "Ценовая категория"
                }
            }
        };

    }
}
