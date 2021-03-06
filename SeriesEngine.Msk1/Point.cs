namespace SeriesEngine.Msk1
{
    using Core.DataAccess;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("msk1.Points")]
    public partial class Point : NamedObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Point()
        {
            //MainHierarchyNodes = new HashSet<MainHierarchyNode>();
            Point_MaxPowers = new HashSet<Point_MaxPower>();
            Point_VoltageLevels = new HashSet<Point_VoltageLevel>();
            Point_PUPlaces = new HashSet<Point_PUPlace>();
            Point_TUCodes = new HashSet<Point_TUCode>();
        }

        public int? AuthorId { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        [MaxLength]
        public string Name { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ConcurrencyStamp { get; set; }

        public int? Tag { get; set; }

        public virtual User User { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<MainHierarchyNode> MainHierarchyNodes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Point_MaxPower> Point_MaxPowers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Point_VoltageLevel> Point_VoltageLevels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Point_PUPlace> Point_PUPlaces { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Point_TUCode> Point_TUCodes { get; set; }

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
