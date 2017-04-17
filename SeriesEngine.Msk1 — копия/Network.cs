namespace SeriesEngine.Msk1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public abstract partial class Network
    {
        public Network()
        {
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
                
        public string Description { get; set; }

        public bool? IsSystem { get; set; }

        public int? CollectionType { get; set; }

        public int SolutionId { get; set; }

        public int Revision { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ConcurrencyStamp { get; set; }

        public int? Tag { get; set; }


        [NotMapped]
        public abstract ICollection<NetworkTreeNode> MyNodes { get; }

        public virtual Solution Solution { get; set; }
    }
}
