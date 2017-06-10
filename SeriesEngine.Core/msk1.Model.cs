﻿// <autogenerated>
//   This file was generated by T4 code generator BuildMetamodels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Data.Entity;
using SeriesEngine.Core.DataAccess;

namespace SeriesEngine.msk1
{
    public partial class Model1 : BaseModelContext
    {
        public Model1() : base("name=Model1")
        {
        }
 
        public virtual DbSet<MainHierarchyNode> MainHierarchyNodes { get; set; }
         public virtual DbSet<SupplierHierarchyNode> SupplierHierarchyNodes { get; set; }
         public virtual DbSet<CurcuitHierarchyNode> CurcuitHierarchyNodes { get; set; }
         public virtual DbSet<Region> Regions { get; set; }
         public virtual DbSet<Consumer> Consumers { get; set; }
         public virtual DbSet<Contract> Contracts { get; set; }
         public virtual DbSet<ConsumerObject> ConsumerObjects { get; set; }
         public virtual DbSet<Point> Points { get; set; }
 		public virtual DbSet<Point_VoltageLevel> Point_VoltageLevels { get; set; }
		public virtual DbSet<Point_MaxPower> Point_MaxPowers { get; set; }
		public virtual DbSet<Point_TUCode> Point_TUCodes { get; set; }
		public virtual DbSet<Point_BPGroup> Point_BPGroups { get; set; }
		public virtual DbSet<Point_PUPlace> Point_PUPlaces { get; set; }
		public virtual DbSet<Point_ContractPriceCategory> Point_ContractPriceCategorys { get; set; }
        public virtual DbSet<ElectricMeter> ElectricMeters { get; set; }
 		public virtual DbSet<ElectricMeter_Direction> ElectricMeter_Directions { get; set; }
		public virtual DbSet<ElectricMeter_Integral> ElectricMeter_Integrals { get; set; }
		public virtual DbSet<ElectricMeter_CoeffOfTransformation> ElectricMeter_CoeffOfTransformations { get; set; }
		public virtual DbSet<ElectricMeter_AdditionInPercent> ElectricMeter_AdditionInPercents { get; set; }
		public virtual DbSet<ElectricMeter_Addition> ElectricMeter_Additions { get; set; }
		public virtual DbSet<ElectricMeter_Odn> ElectricMeter_Odns { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
         public virtual DbSet<SupplierContract> SupplierContracts { get; set; }
         public virtual DbSet<Curcuit> Curcuits { get; set; }
         public virtual DbSet<CurcuitContract> CurcuitContracts { get; set; }
  
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Solution>()
                .HasMany(e => e.Networks)
                .WithRequired(e => e.Solution)
                .WillCascadeOnDelete(true);

			 modelBuilder.Entity<Network>()
                 .Map<MainHierarchyNetwork>(m => m.Requires("NodeType").HasValue("msk1.MainHierarchyNode"))
                 .Map<SupplierHierarchyNetwork>(m => m.Requires("NodeType").HasValue("msk1.SupplierHierarchyNode"))
                 .Map<CurcuitHierarchyNetwork>(m => m.Requires("NodeType").HasValue("msk1.CurcuitHierarchyNode"))
			;

			// TODO probable this does not matter
            modelBuilder.Entity<MainHierarchyNode>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("msk1.MainHierarchyNodes");
                });

             modelBuilder.Entity<MainHierarchyNode>()
                .HasMany(e => e.Children)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentId);
 
			modelBuilder.Entity<MainHierarchyNode>()
                .HasOptional(e => e.Region)
                .WithMany()
                .HasForeignKey(e => e.Region_Id);

			modelBuilder.Entity<MainHierarchyNode>()
                .HasOptional(e => e.Consumer)
                .WithMany()
                .HasForeignKey(e => e.Consumer_Id);

			modelBuilder.Entity<MainHierarchyNode>()
                .HasOptional(e => e.Contract)
                .WithMany()
                .HasForeignKey(e => e.Contract_Id);

			modelBuilder.Entity<MainHierarchyNode>()
                .HasOptional(e => e.ConsumerObject)
                .WithMany()
                .HasForeignKey(e => e.ConsumerObject_Id);

			modelBuilder.Entity<MainHierarchyNode>()
                .HasOptional(e => e.Point)
                .WithMany()
                .HasForeignKey(e => e.Point_Id);

			modelBuilder.Entity<MainHierarchyNode>()
                .HasOptional(e => e.ElectricMeter)
                .WithMany()
                .HasForeignKey(e => e.ElectricMeter_Id);

			// TODO probable this does not matter
            modelBuilder.Entity<SupplierHierarchyNode>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("msk1.SupplierHierarchyNodes");
                });

             modelBuilder.Entity<SupplierHierarchyNode>()
                .HasMany(e => e.Children)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentId);
 
			modelBuilder.Entity<SupplierHierarchyNode>()
                .HasOptional(e => e.Supplier)
                .WithMany()
                .HasForeignKey(e => e.Supplier_Id);

			modelBuilder.Entity<SupplierHierarchyNode>()
                .HasOptional(e => e.SupplierContract)
                .WithMany()
                .HasForeignKey(e => e.SupplierContract_Id);

			modelBuilder.Entity<SupplierHierarchyNode>()
                .HasOptional(e => e.Point)
                .WithMany()
                .HasForeignKey(e => e.Point_Id);

			// TODO probable this does not matter
            modelBuilder.Entity<CurcuitHierarchyNode>()
                .Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("msk1.CurcuitHierarchyNodes");
                });

             modelBuilder.Entity<CurcuitHierarchyNode>()
                .HasMany(e => e.Children)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentId);
 
			modelBuilder.Entity<CurcuitHierarchyNode>()
                .HasOptional(e => e.Curcuit)
                .WithMany()
                .HasForeignKey(e => e.Curcuit_Id);

			modelBuilder.Entity<CurcuitHierarchyNode>()
                .HasOptional(e => e.CurcuitContract)
                .WithMany()
                .HasForeignKey(e => e.CurcuitContract_Id);

			modelBuilder.Entity<CurcuitHierarchyNode>()
                .HasOptional(e => e.Point)
                .WithMany()
                .HasForeignKey(e => e.Point_Id);

			modelBuilder.Entity<Region>()
                .MapToStoredProcedures(s =>
					{
						s.Update(u => u.HasName("msk1.Region_Update"));  
						s.Delete(d => d.HasName("msk1.Region_Delete")); 
						s.Insert(i => i.HasName("msk1.Region_Insert"));
					});

            modelBuilder.Entity<Region>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Region>()
                .HasOptional(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<Region>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
 
			modelBuilder.Entity<Consumer>()
                .MapToStoredProcedures(s =>
					{
						s.Update(u => u.HasName("msk1.Consumer_Update"));  
						s.Delete(d => d.HasName("msk1.Consumer_Delete")); 
						s.Insert(i => i.HasName("msk1.Consumer_Insert"));
					});

            modelBuilder.Entity<Consumer>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Consumer>()
                .HasOptional(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<Consumer>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
 
			modelBuilder.Entity<Contract>()
                .MapToStoredProcedures(s =>
					{
						s.Update(u => u.HasName("msk1.Contract_Update"));  
						s.Delete(d => d.HasName("msk1.Contract_Delete")); 
						s.Insert(i => i.HasName("msk1.Contract_Insert"));
					});

            modelBuilder.Entity<Contract>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Contract>()
                .HasOptional(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<Contract>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
 
			modelBuilder.Entity<ConsumerObject>()
                .MapToStoredProcedures(s =>
					{
						s.Update(u => u.HasName("msk1.ConsumerObject_Update"));  
						s.Delete(d => d.HasName("msk1.ConsumerObject_Delete")); 
						s.Insert(i => i.HasName("msk1.ConsumerObject_Insert"));
					});

            modelBuilder.Entity<ConsumerObject>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ConsumerObject>()
                .HasOptional(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<ConsumerObject>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
 
			modelBuilder.Entity<Point>()
                .MapToStoredProcedures(s =>
					{
						s.Update(u => u.HasName("msk1.Point_Update"));  
						s.Delete(d => d.HasName("msk1.Point_Delete")); 
						s.Insert(i => i.HasName("msk1.Point_Insert"));
					});

            modelBuilder.Entity<Point>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Point>()
                .HasOptional(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<Point>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
 
            modelBuilder.Entity<Point>()
                .HasMany(e => e.Point_VoltageLevels)
                .WithRequired(e => e.Point)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.Point_MaxPowers)
                .WithRequired(e => e.Point)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.Point_TUCodes)
                .WithRequired(e => e.Point)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.Point_BPGroups)
                .WithRequired(e => e.Point)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.Point_PUPlaces)
                .WithRequired(e => e.Point)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.Point_ContractPriceCategorys)
                .WithRequired(e => e.Point)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

			modelBuilder.Entity<ElectricMeter>()
                .MapToStoredProcedures(s =>
					{
						s.Update(u => u.HasName("msk1.ElectricMeter_Update"));  
						s.Delete(d => d.HasName("msk1.ElectricMeter_Delete")); 
						s.Insert(i => i.HasName("msk1.ElectricMeter_Insert"));
					});

            modelBuilder.Entity<ElectricMeter>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ElectricMeter>()
                .HasOptional(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<ElectricMeter>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
 
            modelBuilder.Entity<ElectricMeter>()
                .HasMany(e => e.ElectricMeter_Directions)
                .WithRequired(e => e.ElectricMeter)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ElectricMeter>()
                .HasMany(e => e.ElectricMeter_Integrals)
                .WithRequired(e => e.ElectricMeter)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ElectricMeter>()
                .HasMany(e => e.ElectricMeter_CoeffOfTransformations)
                .WithRequired(e => e.ElectricMeter)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ElectricMeter>()
                .HasMany(e => e.ElectricMeter_AdditionInPercents)
                .WithRequired(e => e.ElectricMeter)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ElectricMeter>()
                .HasMany(e => e.ElectricMeter_Additions)
                .WithRequired(e => e.ElectricMeter)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ElectricMeter>()
                .HasMany(e => e.ElectricMeter_Odns)
                .WithRequired(e => e.ElectricMeter)
                .HasForeignKey(e => e.ObjectId)
                .WillCascadeOnDelete(false);

			modelBuilder.Entity<Supplier>()
                .MapToStoredProcedures(s =>
					{
						s.Update(u => u.HasName("msk1.Supplier_Update"));  
						s.Delete(d => d.HasName("msk1.Supplier_Delete")); 
						s.Insert(i => i.HasName("msk1.Supplier_Insert"));
					});

            modelBuilder.Entity<Supplier>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Supplier>()
                .HasOptional(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
 
			modelBuilder.Entity<SupplierContract>()
                .MapToStoredProcedures(s =>
					{
						s.Update(u => u.HasName("msk1.SupplierContract_Update"));  
						s.Delete(d => d.HasName("msk1.SupplierContract_Delete")); 
						s.Insert(i => i.HasName("msk1.SupplierContract_Insert"));
					});

            modelBuilder.Entity<SupplierContract>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SupplierContract>()
                .HasOptional(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<SupplierContract>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
 
			modelBuilder.Entity<Curcuit>()
                .MapToStoredProcedures(s =>
					{
						s.Update(u => u.HasName("msk1.Curcuit_Update"));  
						s.Delete(d => d.HasName("msk1.Curcuit_Delete")); 
						s.Insert(i => i.HasName("msk1.Curcuit_Insert"));
					});

            modelBuilder.Entity<Curcuit>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Curcuit>()
                .HasOptional(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<Curcuit>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
 
			modelBuilder.Entity<CurcuitContract>()
                .MapToStoredProcedures(s =>
					{
						s.Update(u => u.HasName("msk1.CurcuitContract_Update"));  
						s.Delete(d => d.HasName("msk1.CurcuitContract_Delete")); 
						s.Insert(i => i.HasName("msk1.CurcuitContract_Insert"));
					});

            modelBuilder.Entity<CurcuitContract>()
                .HasRequired(e => e.Solution)
                .WithMany()
                .HasForeignKey(e => e.SolutionId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CurcuitContract>()
                .HasOptional(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<CurcuitContract>()
                .Property(e => e.ConcurrencyStamp)
                .IsFixedLength();
 
		}
	}
}
