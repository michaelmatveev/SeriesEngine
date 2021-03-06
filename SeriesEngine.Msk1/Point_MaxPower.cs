using SeriesEngine.Core.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesEngine.Msk1
{

    [Table("msk1.Point_MaxPowers")]
    public partial class Point_MaxPower : PeriodVariable
    {
        public virtual User User { get; set; }

        [Required]
        [StringLength(200)]
        public string MaxPower { get; set; }

        public virtual Point Point { get; set; }

        public override object Value
        {
            get
            {
                return MaxPower;
            }
            set
            {
                MaxPower = (string)value;
            }
        }
    }
}
