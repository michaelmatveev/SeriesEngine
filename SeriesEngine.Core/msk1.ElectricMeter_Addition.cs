﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using SeriesEngine.Core.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesEngine.msk1
{

    [Table("msk1.ElectricMeter_Additions")]
    public partial class ElectricMeter_Addition : VersionedVariable
    {
        [Required]
        //[StringLength(200)]
        public Double Addition { get; set; }

        public virtual ElectricMeter ElectricMeter { get; set; }

        public override object Value
        {
            get
            {
                return Addition;
            }
            set
            {
                Addition = (Double)value;
            }
        }
    }
}