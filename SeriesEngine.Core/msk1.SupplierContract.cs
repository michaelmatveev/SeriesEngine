﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using SeriesEngine.Core.DataAccess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesEngine.msk1
{
    [Table("msk1.SupplierContracts")]
	public partial class SupplierContract : NamedObject
	{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierContract()
        {
			ObjectModel = msk1Objects.SupplierContract;
		}

        public override string GetName()
        {
            return Name;
        }

        public override void SetName(string newName)
        {
            Name = newName;
        }
	}
}
