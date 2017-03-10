namespace SeriesEngine.Msk1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pwk1.Point_VoltageLevels")]
    public partial class Point_VoltageLevel
    {
        public int Id { get; set; }

        public int ObjectId { get; set; }

        public short? Kind { get; set; }

        public DateTime Date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationTime { get; set; }

        public int? AuthorId { get; set; }

        [Required]
        [StringLength(200)]
        public string VoltageLevel { get; set; }

        public int? Tag { get; set; }

        public virtual User User { get; set; }

        public virtual Point Point { get; set; }
    }
}
