﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.msk1
{
    [Table("msk1.ConsumerObjects")]
	public partial class ConsumerObject : NamedObject
	{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ConsumerObject() : base(msk1Objects.ConsumerObject)
        {
		}

        public override string GetName()
        {
            return Name;
        }

        public override void SetName(string newName)
        {
            Name = newName;
        }

		public static object NameParser(string value) 
		{
			return value;
		}
	}
}
