using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.Core.DataAccess
{
    public partial class StoredQuery : IStateObject
    {
        public StoredQuery()
        {
            State = ObjectState.Unchanged;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ModelName { get; set; }

        public string Text { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] ConcurrencyStamp { get; set; }

        public int? AuthorId { get; set; }

        public virtual User Author { get; set; }

        [NotMapped]
        public ObjectState State { get; set; }
    }
}
