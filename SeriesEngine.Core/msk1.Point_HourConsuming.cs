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

    [Table("msk1.Point_HourConsumings")]
    public partial class Point_HourConsuming : PeriodVariable
    {
        [Required]
        //[StringLength(200)]
        public Double HourConsuming { get; set; }

        public virtual Point Point { get; set; }

        public override object Value
        {
            get
            {
                return HourConsuming;
            }
            set
            {
                HourConsuming = (Double)value;
            }
        }
    }
}
