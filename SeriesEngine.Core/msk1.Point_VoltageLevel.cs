﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using SeriesEngine.Core.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesEngine.msk1
{

    [Table("msk1.Point_VoltageLevels")]
    public partial class Point_VoltageLevel : PeriodVariable
    {
        public virtual User User { get; set; }

        [Required]
        [StringLength(200)]
        public string VoltageLevel { get; set; }

        public virtual Point Point { get; set; }

        public override object Value
        {
            get
            {
                return VoltageLevel;
            }
            set
            {
                VoltageLevel = (string)value;
            }
        }
    }
}
