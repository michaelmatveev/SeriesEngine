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
    [Table("msk1.CurcuitHierarchyNodes")]
    public partial class CurcuitHierarchyNode : NetworkTreeNode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CurcuitHierarchyNode()
        {
            Children = new HashSet<CurcuitHierarchyNode>();
        }

        public int NetId { get; set; }
        [ForeignKey("NetId")]
        public virtual CurcuitHierarchyNetwork Network { get; set; }

        public override Network MyNetwork
        {
            get
            {
                return Network;
            }
            set
            {
                Network = (CurcuitHierarchyNetwork)value;
            }
        }

        public override NetworkTreeNode MyParent
        {
            get
            {
                if (Parent == null)
                {
                    return null;
                }
                return Parent.LinkedObject != null ? Parent : Parent.MyParent;
            }
            set
            {
                Parent = (CurcuitHierarchyNode)value;
            }
        }

        public int? ParentId { get; set; }

        public virtual CurcuitHierarchyNode Parent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurcuitHierarchyNode> Children { get; set; }

		public int? Tag { get; set; }


        public int? Curcuit_Id { get; set; }
        public virtual Curcuit Curcuit { get; set; }

        public int? CurcuitContract_Id { get; set; }
        public virtual CurcuitContract CurcuitContract { get; set; }

        public int? Point_Id { get; set; }
        public virtual Point Point { get; set; }

        public override NamedObject LinkedObject
        {
            get
            {
                if (Curcuit != null)
                {
                    return Curcuit;
                }
                if (CurcuitContract != null)
                {
                    return CurcuitContract;
                }
                if (Point != null)
                {
                    return Point;
                }
                return null;
            }
        }
	}
}

