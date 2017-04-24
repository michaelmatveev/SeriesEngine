using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesEngine.Core.DataAccess
{
    public abstract class VersionedVariable : IStateObject
    {
        public VersionedVariable()
        {
            State = ObjectState.Unchanged;
        }

        [NotMapped]
        public ObjectState State { get; set; }

        public int Id { get; set; }
        public int ObjectId { get; set; }
        public short? Kind { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationTime { get; set; }
        public int? AuthorId { get; set; }
        public int? Tag { get; set; }
        public virtual User Author { get; set; }
        public abstract object Value { get; set; }

    }
}
