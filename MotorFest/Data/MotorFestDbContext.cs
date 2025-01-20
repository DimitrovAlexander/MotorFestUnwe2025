using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotorFest.Data.Entities;

namespace MotorFest.Data
{
    public class MotorFestDbContext : IdentityDbContext<MFUser,IdentityRole,string>
    {
        public MotorFestDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }

        public virtual DbSet<EngineType> EngineTypes { get; set; }

        public virtual DbSet<Event> Events { get; set; }


        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<VehicleCategory> VehicleCategories { get; set; }
        public virtual DbSet<EventVehicleCategory> EventVehicleCategories { get; set; }
        public virtual DbSet<EventEngineType> EventEngineTypes { get; set; }

        public virtual DbSet<EventRegistration> EventRegistrations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Определяне на първичен ключ за таблицата EventVehicleCategory
            modelBuilder.Entity<EventVehicleCategory>()
    .HasKey(evc => new { evc.EventId, evc.VehicleCategoryId }); // Composite ключ

            modelBuilder.Entity<EventVehicleCategory>()
                .HasOne(evc => evc.Event)
                .WithMany(e => e.EventVehicleCategories)
                .HasForeignKey(evc => evc.EventId);

            modelBuilder.Entity<EventVehicleCategory>()
                .HasOne(evc => evc.VehicleCategory)
                .WithMany(vc => vc.EventVehicleCategories)
                .HasForeignKey(evc => evc.VehicleCategoryId); 
            modelBuilder.Entity<EventEngineType>()
    .HasKey(evc => new { evc.EventId, evc.EngineTypeId }); // Composite ключ

            modelBuilder.Entity<EventEngineType>()
                .HasOne(evc => evc.Event)
                .WithMany(e => e.EventEngineTypes)
                .HasForeignKey(evc => evc.EventId);

            modelBuilder.Entity<EventEngineType>()
                .HasOne(evc => evc.EngineType)
                .WithMany(et => et.EventEngineTypes)
                .HasForeignKey(evc => evc.EngineTypeId);
            modelBuilder.Entity<EventRegistration>()
    .HasKey(evc => new { evc.EventId, evc.VehicleId }); // Composite ключ

            modelBuilder.Entity<EventRegistration>()
                .HasOne(evc => evc.Event)
                .WithMany(evc => evc.EventRegistration)
                .HasForeignKey(evc => evc.EventId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventRegistration>()
                .HasOne(evc => evc.Vehicle)
                .WithMany(et => et.EventRegistration)
                .HasForeignKey(evc => evc.VehicleId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasDefaultSchema("21180022");

            //modelBuilder.Entity<Location>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__Locatione__3214EC07EAB1D04B");

            //    entity.ToTable("Locationes", "21180022");

            //    entity.Property(e => e.FullAddress)
            //        .HasMaxLength(100)
            //        .HasColumnName("Location");
            //    entity.Property(e => e.City).HasMaxLength(50);
            //    entity.Property(e => e.LastUpdate)
            //        .HasDefaultValueSql("(getdate())")
            //        .HasColumnType("datetime");
            //    entity.Property(e => e.Municipality).HasMaxLength(50);
            //});

            //modelBuilder.Entity<AuditLog>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__AuditLog__3214EC077CC0A27E");

            //    entity.ToTable("AuditLog", "21180022");

            //    entity.Property(e => e.Action).HasMaxLength(50);
            //    entity.Property(e => e.ChangeTime)
            //        .HasDefaultValueSql("(getdate())")
            //        .HasColumnType("datetime");
            //    entity.Property(e => e.TableName).HasMaxLength(100);
            //});

            //modelBuilder.Entity<EngineType>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__EngineTy__3214EC07BD6054E7");

            //    entity.ToTable("EngineType", "21180022");

            //    entity.HasIndex(e => e.CategoryName, "UQ__EngineTy__737584F6301C62FE").IsUnique();

            //    entity.Property(e => e.LastUpdate)
            //        .HasDefaultValueSql("(getdate())")
            //        .HasColumnType("datetime");
            //    entity.Property(e => e.CategoryName).HasMaxLength(50);
            //});

            //modelBuilder.Entity<Event>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__Events__3214EC0767B9445D");

            //    entity.ToTable("Events", "21180022");

            //    entity.Property(e => e.EntranceFee).HasColumnType("decimal(10, 2)");
            //    entity.Property(e => e.EventDate).HasColumnType("datetime");
            //    entity.Property(e => e.LastUpdate)
            //        .HasDefaultValueSql("(getdate())")
            //        .HasColumnType("datetime");

            //    entity.HasOne(d => d.Location).WithMany(p => p.Events)
            //        .HasForeignKey(d => d.LocationId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__Events__LocationI__4AB81AF0");

            //    entity.HasOne(d => d.Category).WithMany(p => p.Events)
            //        .HasForeignKey(d => d.CategoryId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__Events__Category__4BAC3F29");

            //    entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
            //        .HasForeignKey(d => d.OrganizerId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__Events__Organize__49C3F6B7");
            //});



            //modelBuilder.Entity<Vehicle>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__Vehicles__3214EC070FF93791");

            //    entity.ToTable("Vehicles", "21180022", tb => tb.HasTrigger("trg_Vehicles"));

            //    entity.Property(e => e.LastUpdate)
            //        .HasDefaultValueSql("(getdate())")
            //        .HasColumnType("datetime");
            //    entity.Property(e => e.Manufacturer).HasMaxLength(100);
            //    entity.Property(e => e.Model).HasMaxLength(100);

            //    entity.HasOne(d => d.Category).WithMany(p => p.Vehicles)
            //        .HasForeignKey(d => d.CategoryId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__Vehicles__Catego__5070F446");

            //    entity.HasOne(d => d.EngineType).WithMany(p => p.Vehicles)
            //        .HasForeignKey(d => d.EngineTypeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__Vehicles__Engine__5165187F");

            //    entity.HasOne(d => d.Owner).WithMany(p => p.Vehicles)
            //        .HasForeignKey(d => d.OwnerId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK__Vehicles__OwnerI__4F7CD00D");
            //});

            //modelBuilder.Entity<VehicleCategory>(entity =>
            //{
            //    entity.HasKey(e => e.Id).HasName("PK__VehicleC__3214EC072E73AF8E");

            //    entity.ToTable("EventVehicleCategories", "21180022");

            //    entity.HasIndex(e => e.CategoryName, "UQ__VehicleC__737584F6A08FB466").IsUnique();

            //    entity.Property(e => e.LastUpdate)
            //        .HasDefaultValueSql("(getdate())")
            //        .HasColumnType("datetime");
            //    entity.Property(e => e.CategoryName).HasMaxLength(50);
            //});
            base.OnModelCreating(modelBuilder);

        }


    }
}
