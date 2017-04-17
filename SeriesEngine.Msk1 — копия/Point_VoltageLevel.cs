namespace SeriesEngine.Msk1
{
    using Core.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
