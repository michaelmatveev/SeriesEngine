namespace SeriesEngine.Msk1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Network> Networks { get; set; }
        public virtual DbSet<Solution> Solutions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ConsumerObject> ConsumerObjects { get; set; }
        public virtual DbSet<Consumer> Consumers { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<MainHierarchy> MainHierarchies { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<MainHierarchy1> MainHierarchy1 { get; set; }
        public virtual DbSet<ObjectA_PeriodDecimalVariables> ObjectA_PeriodDecimalVariables { get; set; }
        public virtual DbSet<ObjectA_PeriodEnumVariables> ObjectA_PeriodEnumVariables { get; set; }
        public virtual DbSet<ObjectA_PeriodIntVariables> ObjectA_PeriodIntVariables { get; set; }
        public virtual DbSet<ObjectA_PeriodSmallIntVariables> ObjectA_PeriodSmallIntVariables { get; set; }
        public virtual DbSet<ObjectA> ObjectAs { get; set; }
        public virtual DbSet<ObjectB> ObjectBs { get; set; }
        public virtual DbSet<ObjectC> ObjectCs { get; set; }
        public virtual DbSet<SecondHierarchy> SecondHierarchies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Network>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Network>()
                .HasMany(e => e.MainHierarchies)
                .WithRequired(e => e.Network)
                .HasForeignKey(e => e.NetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Solution>()
                .HasMany(e => e.Networks)
                .WithRequired(e => e.Solution)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ConsumerObjects)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Consumers)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Contracts)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Points)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Regions)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<ConsumerObject>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<ConsumerObject>()
                .HasMany(e => e.MainHierarchies)
                .WithOptional(e => e.ConsumerObject)
                .HasForeignKey(e => e.ConsumerObject_Id);

            modelBuilder.Entity<Consumer>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<Consumer>()
                .HasMany(e => e.MainHierarchies)
                .WithOptional(e => e.Consumer)
                .HasForeignKey(e => e.Consumer_Id);

            modelBuilder.Entity<Contract>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.MainHierarchies)
                .WithOptional(e => e.Contract)
                .HasForeignKey(e => e.Contract_Id);

            modelBuilder.Entity<MainHierarchy>()
                .HasMany(e => e.MainHierarchy1)
                .WithOptional(e => e.MainHierarchy2)
                .HasForeignKey(e => e.ReplaceId);

            modelBuilder.Entity<Point>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<Point>()
                .HasMany(e => e.MainHierarchies)
                .WithOptional(e => e.Point)
                .HasForeignKey(e => e.Point_Id);

            modelBuilder.Entity<Region>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<Region>()
                .HasMany(e => e.MainHierarchies)
                .WithOptional(e => e.Region)
                .HasForeignKey(e => e.Region_Id);

            modelBuilder.Entity<ObjectA_PeriodDecimalVariables>()
                .Property(e => e.PeriodDecimalVariable)
                .HasPrecision(38, 20);

            modelBuilder.Entity<ObjectA_PeriodIntVariables>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<ObjectA_PeriodSmallIntVariables>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<ObjectA>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<ObjectB>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<ObjectC>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
        }
    }
}
