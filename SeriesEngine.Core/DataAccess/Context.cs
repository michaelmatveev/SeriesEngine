using System.Data.Entity;

namespace SeriesEngine.Core.DataAccess
{
    public class SeContext : DbContext
    {
        public SeContext()
            : base("name=SeriesEngine")
        {
        }

        public DbSet<Solution> Solutions { get; set; }
    }
}
