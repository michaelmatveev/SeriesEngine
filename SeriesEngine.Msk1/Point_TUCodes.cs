namespace SeriesEngine.Msk1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pwk1.Point_TUCodes")]
    public partial class Point_TUCode : PeriodVariable
    {
        public virtual User User { get; set; }

        [StringLength(200)]
        public string TUCode { get; set; }

        public virtual Point Point { get; set; }

        public override object Value
        {
            get
            {
                return TUCode;
            }
        }
    }
}
