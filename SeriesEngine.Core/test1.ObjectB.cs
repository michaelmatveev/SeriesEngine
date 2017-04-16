﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using SeriesEngine.Core.DataAccess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesEngine.test1
{
    [Table("msk1.ObjectB")]
	public partial class ObjectB : NamedObject
	{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ObjectB()
        {
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

		public int SolutionId { get; set; }

        public virtual Solution Solution { get; set; }

        public override string GetName()
        {
            return Name;
        }

        public override void SetName(string newName)
        {
            Name = newName;
        }

        [Required]
        [StringLength(int.MaxValue)]
        [MaxLength]
        public string  Description { get; set; }
	}
}
