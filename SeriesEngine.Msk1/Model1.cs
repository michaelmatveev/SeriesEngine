namespace SeriesEngine.Msk1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Core.DataAccess;
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
        public virtual DbSet<MainHierarchyNode> MainHierarchyNodes { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<ElectricMeter> ElectricMeters { get; set; }
        public virtual DbSet<Point_VoltageLevel> Point_VoltageLevels { get; set; }
        public virtual DbSet<Point_MaxPower> Point_MaxPowers { get; set; }
        public virtual DbSet<Point_PUPlace> Point_PUPlaces { get; set; }
        public virtual DbSet<Point_TUCode> Point_TUCodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<IStateObject>()
            //    .Ignore(s => s.State);

            modelBuilder.Entity<Network>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Network>()
                .HasMany(e => e.Nodes)
                .WithRequired(e => e.Network)
                .HasForeignKey(e => e.NetId)
                .WillCascadeOnDelete(true);//?
            
            modelBuilder.Entity<MainHierarchyNode>()
                .HasMany(e => e.Children)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Solution>()
                .HasMany(e => e.Networks)
                .WithRequired(e => e.Solution)
                .WillCascadeOnDelete(true);

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
                .HasMany(e => e.ElectricMeters)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Regions)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Point_MaxPowers)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Point_VoltageLevels)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<ConsumerObject>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ConsumerObject>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<ConsumerObject>()
                .HasMany(e => e.MainHierarchyNodes)
                .WithOptional(e => e.ConsumerObject)
                .HasForeignKey(e => e.ConsumerObject_Id);

            modelBuilder.Entity<Consumer>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Consumer>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<Consumer>()
                .HasMany(e => e.MainHierarchyNodes)
                .WithOptional(e => e.Consumer)
                .HasForeignKey(e => e.Consumer_Id);

            modelBuilder.Entity<Contract>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Contract>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.MainHierarchyNodes)
                .WithOptional(e => e.Contract)
                .HasForeignKey(e => e.Contract_Id);

            //modelBuilder.Entity<Contract>()
            //    .MapToStoredProcedures(s => s
            //        .Delete(d => d.HasName("pwk1.Contract_Delete"))
            //        .Insert(i => i.HasName("pwk1.Contract_Insert"))
            //        .Update(u => u.HasName("pwk1.Contract_Update")));

            modelBuilder.Entity<ElectricMeter>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ElectricMeter>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<ElectricMeter>()
                .HasMany(e => e.MainHierarchyNodes)
                .WithOptional(e => e.ElectricMeter)
                .HasForeignKey(e => e.ElectricMeter_Id);

            modelBuilder.Entity<Point>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);
            
            modelBuilder.Entity<Point>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<Point>()
                .HasMany(e => e.MainHierarchyNodes)
                .WithOptional(e => e.Point)
                .HasForeignKey(e => e.Point_Id);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.Point_MaxPowers)
                .WithRequired(e => e.Point)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.Point_VoltageLevels)
                .WithRequired(e => e.Point)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.Point_PUPlaces)
                .WithRequired(e => e.Point)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.Point_TUCodes)
                .WithRequired(e => e.Point)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            // TODO generate all stored procedures
            // Imposible to mark only one storepd proc
            // http://stackoverflow.com/questions/27395338/entity-framework-6-map-only-one-statement-to-stored-procedure
            //modelBuilder.Entity<Point>()
            //    .MapToStoredProcedures(s =>
            //        s.Delete(d => d.HasName("pwk1.Point_Delete")
            //            .Parameter(o => o.Id, "Id")));

            modelBuilder.Entity<Region>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Region>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();

            modelBuilder.Entity<Region>()
                .HasMany(e => e.MainHierarchyNodes)
                .WithOptional(e => e.Region)
                .HasForeignKey(e => e.Region_Id);
        }
    }
}
