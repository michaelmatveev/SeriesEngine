namespace SeriesEngine.Msk1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("test1.ObjectA_PeriodDecimalVariables")]
    public partial class ObjectA_PeriodDecimalVariables
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ObjectId { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime Date { get; set; }

        public short? Kind { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime CreationTime { get; set; }

        public int? AuthorId { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal PeriodDecimalVariable { get; set; }

        public int? Tag { get; set; }
    }
}
