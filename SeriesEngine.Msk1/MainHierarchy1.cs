namespace SeriesEngine.Msk1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("test1.MainHierarchy")]
    public partial class MainHierarchy1
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

        public int? ObjectC_Id { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTill { get; set; }

        public int? ReplaceId { get; set; }

        public int? Tag { get; set; }
    }
}
