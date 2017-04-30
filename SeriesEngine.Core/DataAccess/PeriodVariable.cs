using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesEngine.Core.DataAccess
{
    public abstract class PeriodVariable:
        IStateObject
    {
        public PeriodVariable()
        {
            State = ObjectState.Unchanged;
        }

        public int Id { get; set; }

        public int ObjectId { get; set; }

        public short? Kind { get; set; }

        public DateTime Date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationTime { get; set; }

        public int? AuthorId { get; set; }

        public int? Tag { get; set; }

        public virtual User Author { get; set; }

        public abstract object Value { get; set; }

        [NotMapped]
        public ObjectState State { get; set; }

    }
}
