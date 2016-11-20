namespace SeriesEngine.Msk1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("test1.SecondHierarchy")]
    public partial class SecondHierarchy
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NetId { get; set; }

        public int? ObjectA_Id { get; set; }

        public int? ObjectB_Id { get; set; }

        public int? Tag { get; set; }
    }
}
