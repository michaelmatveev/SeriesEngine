using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.Msk1
{
    public abstract class PeriodVariable
    {
        public int Id { get; set; }

        public int ObjectId { get; set; }

        public short? Kind { get; set; }

        public DateTime Date { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationTime { get; set; }

        public int? AuthorId { get; set; }

        public int? Tag { get; set; }

        //public virtual User User { get; set; }

        public abstract object Value { get; }
    }
}
