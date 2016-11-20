namespace SeriesEngine.Msk1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pwk1.Consumers")]
    public partial class Consumer : NamedObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Consumer()
        {
            MainHierarchies = new HashSet<MainHierarchy>();
        }

        public int Id { get; set; }

        public DateTime ObjectCreationTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int? AuthorId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ConcurrencyStamp { get; set; }

        public int? Tag { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainHierarchy> MainHierarchies { get; set; }

        public override string GetName()
        {
            return this.Name;
        }
    }
}
