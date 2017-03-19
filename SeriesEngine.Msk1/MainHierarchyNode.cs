namespace SeriesEngine.Msk1
{
    using Core;
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

        public int? ElectricMeter_Id { get; set; }

        public int? Tag { get; set; }

        public virtual ElectricMeter ElectricMeter { get; set; }

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
                if (ElectricMeter != null)
                {
                    ElectricMeter.ObjectModel = PointModel;
                    return ElectricMeter;
                }

                return null;
            }

        }

        public static ObjectMetamodel RegionModel = new ObjectMetamodel
        {
            Name = "Region",
            Variables = new List<Variable>()
        };

        public static ObjectMetamodel ConsumerModel = new ObjectMetamodel
        {
            Name = "Consumer",
            Variables = new List<Variable>()
        };

        public static ObjectMetamodel ContractModel = new ObjectMetamodel
        {
            Name = "Contract",
            Variables = new List<Variable>
            {
                new Variable
                {
                    Name = "ContractType"
                },
            }
        };

        public static ObjectMetamodel ConsumerObjectModel = new ObjectMetamodel
        {
            Name = "ConsumerObject",
            Variables = new List<Variable>()
        };

        public static ObjectMetamodel PointModel = new ObjectMetamodel
        {
            Name = "Point",
            Variables = new List<Variable>
            {
                new Variable
                {
                    Name = "VoltageLevel",
                    IsPeriodic = true                    
                },
                new Variable
                {
                    Name = "MaxPower",
                    IsPeriodic = true
                },
                new Variable
                {
                    Name = "TUCode",
                    IsPeriodic = true
                },
                new Variable
                {
                    Name = "PUPlace",
                    IsPeriodic = false,
                    IsVersioned = true
                }
            }
        };

        public static ObjectMetamodel ElectricMeterModel = new ObjectMetamodel
        {
            Name = "ElectricMeter",
            Variables = new List<Variable>
            {
                new Variable
                {
                    Name = "PUType"
                }
            }
        };

        public static ObjectMetamodel GetObjectModelByName(string modelName)
        {
            switch (modelName)
            {
                case "Region":
                case "Regions": return MainHierarchyNode.RegionModel;
                case "Consumer":
                case "Consumers": return MainHierarchyNode.ConsumerModel;
                case "Contract":
                case "Contracts": return MainHierarchyNode.ContractModel;
                case "ConsumerObject":
                case "ConsumerObjects": return MainHierarchyNode.ConsumerObjectModel;
                case "Point":
                case "Points": return MainHierarchyNode.PointModel;
                case "ElectricMeter":
                case "ElectricMeters": return MainHierarchyNode.ElectricMeterModel;
                default: throw new NotSupportedException();
            }
        }
    }
}
