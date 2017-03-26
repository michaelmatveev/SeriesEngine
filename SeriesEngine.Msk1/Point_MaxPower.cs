namespace SeriesEngine.Msk1
{
    using Core.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pwk1.Point_MaxPowers")]
    public partial class Point_MaxPower : PeriodVariable
    {
        public virtual User User { get; set; }

        [Required]
        [StringLength(200)]
        public string MaxPower { get; set; }

        public virtual Point Point { get; set; }

        public override object Value
        {
            get
            {
                return MaxPower;
            }
            set
            {
                MaxPower = (string)value;
            }
        }
    }
}
