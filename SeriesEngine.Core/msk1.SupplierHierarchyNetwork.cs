﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Collections.Generic;
using SeriesEngine.Core.Helpers;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.msk1
{
    public class SupplierHierarchyNetwork : Network
    {
        public SupplierHierarchyNetwork() : base(msk1Hierarchies.SupplierHierarchy)
        {
            Nodes = new HashSet<SupplierHierarchyNode>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierHierarchyNode> Nodes { get; set; }

        public override ICollection<NetworkTreeNode> MyNodes
        {
            get
            {
                return Nodes.CastCollection<SupplierHierarchyNode, NetworkTreeNode>();
            }
        }
    }
}

