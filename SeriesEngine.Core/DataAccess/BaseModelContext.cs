using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesEngine.Core.DataAccess
{
    public class BaseModelContext : DbContext
    {
        public BaseModelContext(string connectionString) : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Network> Networks { get; set; }
        public virtual DbSet<Solution> Solutions { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}
