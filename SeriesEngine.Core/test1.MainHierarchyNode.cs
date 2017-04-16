﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using SeriesEngine.Core;
using SeriesEngine.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace SeriesEngine.test1
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


        public int? ObjectA_Id { get; set; }
        public virtual ObjectA ObjectA { get; set; }

        public int? ObjectB_Id { get; set; }
        public virtual ObjectB ObjectB { get; set; }

        public int? ObjectC_Id { get; set; }
        public virtual ObjectC ObjectC { get; set; }

        public override NamedObject LinkedObject
        {
            get
            {
                if (ObjectA != null)
                {
                    //ObjectA.ObjectModel = ObjectAModel;
                    return ObjectA;
                }
                if (ObjectB != null)
                {
                    //ObjectB.ObjectModel = ObjectBModel;
                    return ObjectB;
                }
                if (ObjectC != null)
                {
                    //ObjectC.ObjectModel = ObjectCModel;
                    return ObjectC;
                }
                return null;
            }
        }
	}
}

