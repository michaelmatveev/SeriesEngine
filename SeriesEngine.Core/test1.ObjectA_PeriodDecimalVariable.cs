﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using SeriesEngine.Core.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesEngine.test1
{

    [Table("test1.ObjectA_PeriodDecimalVariables")]
    public partial class ObjectA_PeriodDecimalVariable : PeriodVariable
    {
        public virtual User User { get; set; }

        [Required]
        [StringLength(200)]
        public string PeriodDecimalVariable { get; set; }

        public virtual Point Point { get; set; }

        public override object Value
        {
            get
            {
                return PeriodDecimalVariable;
            }
            set
            {
                PeriodDecimalVariable = (string)value;
            }
        }
    }
}
