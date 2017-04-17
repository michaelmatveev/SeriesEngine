﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace SeriesEngine.msk1
{
    //[Table("msk1.MainHierarchyNodes")]
    public partial class MainHierarchyNode : NetworkTreeNode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MainHierarchyNode()
        {
            Children = new HashSet<MainHierarchyNode>();
        }

        public int NetId { get; set; }
        [ForeignKey("NetId")]
        public virtual MainHierarchyNetwork Network { get; set; }

        public override Network MyNetwork
        {
            get
            {
                return Network;
            }
            set
            {
                Network = (MainHierarchyNetwork)value;
            }
        }

        public override NetworkTreeNode MyParent
        {
            get
            {
                return Parent;
            }
            set
            {
                Parent = (MainHierarchyNode)value;
            }
        }

        public int? ParentId { get; set; }

        public virtual MainHierarchyNode Parent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainHierarchyNode> Children { get; set; }

		public int? Tag { get; set; }


        public int? Region_Id { get; set; }
        public virtual Region Region { get; set; }

        public int? Consumer_Id { get; set; }
        public virtual Consumer Consumer { get; set; }

        public int? Contract_Id { get; set; }
        public virtual Contract Contract { get; set; }

        public int? ConsumerObject_Id { get; set; }
        public virtual ConsumerObject ConsumerObject { get; set; }

        public int? Point_Id { get; set; }
        public virtual Point Point { get; set; }

        public int? ElectricMeter_Id { get; set; }
        public virtual ElectricMeter ElectricMeter { get; set; }

        public override NamedObject LinkedObject
        {
            get
            {
                if (Region != null)
                {
                    //Region.ObjectModel = RegionModel;
                    return Region;
                }
                if (Consumer != null)
                {
                    //Consumer.ObjectModel = ConsumerModel;
                    return Consumer;
                }
                if (Contract != null)
                {
                    //Contract.ObjectModel = ContractModel;
                    return Contract;
                }
                if (ConsumerObject != null)
                {
                    //ConsumerObject.ObjectModel = ConsumerObjectModel;
                    return ConsumerObject;
                }
                if (Point != null)
                {
                    //Point.ObjectModel = PointModel;
                    return Point;
                }
                if (ElectricMeter != null)
                {
                    //ElectricMeter.ObjectModel = ElectricMeterModel;
                    return ElectricMeter;
                }
                return null;
            }
        }
	}
}
