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

    [Table("msk1.ElectricMeter_LossTransports")]
    public partial class ElectricMeter_LossTransport : PeriodVariable
    {
        [Required]
        //[StringLength(200)]
        public Double LossTransport { get; set; }

        public virtual ElectricMeter ElectricMeter { get; set; }

        public override object Value
        {
            get
            {
                return LossTransport;
            }
            set
            {
                LossTransport = (Double)value;
            }
        }
    }
}