namespace SeriesEngine.Msk1
{
    using Core.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("msk1.Contracts")]
    public partial class Contract : NamedObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contract()
        {
            //MainHierarchyNodes = new HashSet<MainHierarchyNode>();
        }

        public int? AuthorId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        [MaxLength]
        public string ContractType { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ConcurrencyStamp { get; set; }

        public int? Tag { get; set; }

        public virtual User User { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<MainHierarchyNode> MainHierarchyNodes { get; set; }

        public int SolutionId { get; set; }
        public virtual Solution Solution { get; set; }

        public override string GetName()
        {
           return this.Name;
        }

        public override void SetName(string newName)
        {
            this.Name = newName;
        }

    }
}
