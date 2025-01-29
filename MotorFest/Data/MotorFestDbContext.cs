using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotorFest.Data.Entities;
using MotorFest.Models.Event;

namespace MotorFest.Data
{
    //Scaffold-DbContext "Name=DefaultConnection" Microsoft.EntityFrameworkCore.SqlServer -ContextDir Data -OutputDir Data.Entities -f -Context MotorFestDbContext
    public class MotorFestDbContext : IdentityDbContext<MFUser,IdentityRole,string>
    {
        public MotorFestDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<log_21180022> log_21180022 { get; set; }

        public virtual DbSet<EngineType> EngineTypes { get; set; }

        public virtual DbSet<Event> Events { get; set; }


        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<VehicleCategory> VehicleCategories { get; set; }
        public virtual DbSet<EventVehicleCategory> EventVehicleCategories { get; set; }
        public virtual DbSet<EventEngineType> EventEngineTypes { get; set; }

        public virtual DbSet<EventRegistration> EventRegistrations { get; set; }
        public virtual DbSet<UsersView> UsersView { get; set; }
        public virtual DbSet<EventsView> EventsView { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Определяне на първичен ключ за таблицата EventVehicleCategory
            modelBuilder.Entity<EventVehicleCategory>()
    .HasKey(evc => new { evc.EventId, evc.VehicleCategoryId }); // Composite ключ

            modelBuilder.Entity<EventVehicleCategory>()
                .ToTable(tb => tb.UseSqlOutputClause(false))
                .HasOne(evc => evc.Event)
                .WithMany(e => e.EventVehicleCategories)
                .HasForeignKey(evc => evc.EventId);

            modelBuilder.Entity<EventVehicleCategory>()
                                .ToTable(tb => tb.UseSqlOutputClause(false))
                .HasOne(evc => evc.VehicleCategory)
                .WithMany(vc => vc.EventVehicleCategories)
                .HasForeignKey(evc => evc.VehicleCategoryId); 
            modelBuilder.Entity<EventEngineType>()
    .HasKey(evc => new { evc.EventId, evc.EngineTypeId }); // Composite ключ

            modelBuilder.Entity<EventEngineType>()
                                .ToTable(tb => tb.UseSqlOutputClause(false))
                .HasOne(evc => evc.Event)
                .WithMany(e => e.EventEngineTypes)
                .HasForeignKey(evc => evc.EventId);

            modelBuilder.Entity<EventEngineType>()
                                .ToTable(tb => tb.UseSqlOutputClause(false))
                .HasOne(evc => evc.EngineType)
                .WithMany(et => et.EventEngineTypes)
                .HasForeignKey(evc => evc.EngineTypeId);
            modelBuilder.Entity<EventRegistration>()
    .HasKey(evc => new { evc.EventId, evc.VehicleId }); // Composite ключ

            modelBuilder.Entity<EventRegistration>()
                                .ToTable(tb => tb.UseSqlOutputClause(false))
                .HasOne(evc => evc.Event)
                .WithMany(evc => evc.EventRegistration)
                .HasForeignKey(evc => evc.EventId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventRegistration>()
                                .ToTable(tb => tb.UseSqlOutputClause(false))
                .HasOne(evc => evc.Vehicle)
                .WithMany(et => et.EventRegistration)
                .HasForeignKey(evc => evc.VehicleId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>()
                    .ToTable(tb => tb.UseSqlOutputClause(false));
            modelBuilder.Entity<Vehicle>()
        .ToTable(tb => tb.UseSqlOutputClause(false));
            modelBuilder.Entity<Event>()
        .ToTable(tb => tb.UseSqlOutputClause(false));




            modelBuilder.Entity<UsersView>(entity =>
            {
                entity.HasNoKey(); // View нямат първичен ключ
                entity.ToView("UsersView"); // Свържете модела с View-то
            });
            modelBuilder.Entity<EventsView>(entity =>
            {
                entity.HasNoKey(); // View нямат първичен ключ
                entity.ToView("EventsView"); // Свържете модела с View-то
            });
            modelBuilder.HasDefaultSchema("21180022");

            //modelBuilder.Entity<EngineType>(entity =>
            //{
            //    entity.ToTable("EngineTypes", "21180022");

            //    entity.Property(e => e.LastUpdate).HasColumnName("21180022_LastUpdate");
            //});

            //    modelBuilder.Entity<Event>(entity =>
            //    {
            //        entity.ToTable("Events", "21180022", tb => tb.HasTrigger("trg_AfterUpdate_Events"));

            //        entity.HasIndex(e => e.LocationId, "IX_Events_LocationId");

            //        entity.HasIndex(e => e.OrganizerId, "IX_Events_OrganizerId");

            //        entity.Property(e => e.EntranceFee).HasColumnType("decimal(18, 2)");
            //        entity.Property(e => e._21180022LastUpdate).HasColumnName("21180022_LastUpdate");

            //        entity.HasOne(d => d.Location).WithMany(p => p.Events).HasForeignKey(d => d.LocationId);

            //        entity.HasOne(d => d.Organizer).WithMany(p => p.Events).HasForeignKey(d => d.OrganizerId);

            //        entity.HasMany(d => d.EngineTypes).WithMany(p => p.Events)
            //            .UsingEntity<Dictionary<string, object>>(
            //                "EventEngineType",
            //                r => r.HasOne<EngineType>().WithMany().HasForeignKey("EngineTypeId"),
            //                l => l.HasOne<Event>().WithMany().HasForeignKey("EventId"),
            //                j =>
            //                {
            //                    j.HasKey("EventId", "EngineTypeId");
            //                    j.ToTable("EventEngineTypes", "21180022");
            //                    j.HasIndex(new[] { "EngineTypeId" }, "IX_EventEngineTypes_EngineTypeId");
            //                });

            //        entity.HasMany(d => d.VehicleCategories).WithMany(p => p.Events)
            //            .UsingEntity<Dictionary<string, object>>(
            //                "EventVehicleCategory",
            //                r => r.HasOne<VehicleCategory>().WithMany().HasForeignKey("VehicleCategoryId"),
            //                l => l.HasOne<Event>().WithMany().HasForeignKey("EventId"),
            //                j =>
            //                {
            //                    j.HasKey("EventId", "VehicleCategoryId");
            //                    j.ToTable("EventVehicleCategories", "21180022");
            //                    j.HasIndex(new[] { "VehicleCategoryId" }, "IX_EventVehicleCategories_VehicleCategoryId");
            //                });
            //    });

            //    modelBuilder.Entity<EventRegistration>(entity =>
            //    {
            //        entity.HasKey(e => new { e.EventId, e.VehicleId });

            //        entity.ToTable("EventRegistrations", "21180022");

            //        entity.HasIndex(e => e.VehicleId, "IX_EventRegistrations_VehicleId");

            //        entity.HasOne(d => d.Event).WithMany(p => p.EventRegistrations)
            //            .HasForeignKey(d => d.EventId)
            //            .OnDelete(DeleteBehavior.ClientSetNull);

            //        entity.HasOne(d => d.Vehicle).WithMany(p => p.EventRegistrations)
            //            .HasForeignKey(d => d.VehicleId)
            //            .OnDelete(DeleteBehavior.ClientSetNull);
            //    });

            //    modelBuilder.Entity<Location>(entity =>
            //    {
            //        entity.ToTable("Locations", "21180022");

            //        entity.Property(e => e._21180022LastUpdate).HasColumnName("21180022_LastUpdate");
            //    });

            //    modelBuilder.Entity<Log21180022>(entity =>
            //    {
            //        entity.ToTable("log_21180022", "21180022");

            //        entity.Property(e => e._21180022LastUpdate).HasColumnName("21180022_LastUpdate");
            //    });

            //    modelBuilder.Entity<Vehicle>(entity =>
            //    {
            //        entity.ToTable("Vehicles", "21180022");

            //        entity.HasIndex(e => e.CategoryId, "IX_Vehicles_CategoryId");

            //        entity.HasIndex(e => e.EngineTypeId, "IX_Vehicles_EngineTypeId");

            //        entity.HasIndex(e => e.OwnerId, "IX_Vehicles_OwnerId");

            //        entity.Property(e => e._21180022LastUpdate).HasColumnName("21180022_LastUpdate");

            //        entity.HasOne(d => d.Category).WithMany(p => p.Vehicles).HasForeignKey(d => d.CategoryId);

            //        entity.HasOne(d => d.EngineType).WithMany(p => p.Vehicles).HasForeignKey(d => d.EngineTypeId);

            //        entity.HasOne(d => d.Owner).WithMany(p => p.Vehicles).HasForeignKey(d => d.OwnerId);
            //    });

            //    modelBuilder.Entity<VehicleCategory>(entity =>
            //    {
            //        entity.ToTable("VehicleCategories", "21180022");

            //        entity.Property(e => e._21180022LastUpdate).HasColumnName("21180022_LastUpdate");
            //    });

            base.OnModelCreating(modelBuilder);

        }


    }
}
