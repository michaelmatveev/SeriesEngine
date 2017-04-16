using System.ComponentModel.DataAnnotations;

namespace SeriesEngine.Core.DataAccess
{
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string LastName { get; set; }

        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(128)]
        public string MiddleName { get; set; }

        public bool? IsSystem { get; set; }

    }
}
