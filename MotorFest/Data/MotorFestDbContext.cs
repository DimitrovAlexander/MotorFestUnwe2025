using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
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

            
            modelBuilder.Entity<EventVehicleCategory>()
    .HasKey(evc => new { evc.EventId, evc.VehicleCategoryId }); 

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
    .HasKey(evc => new { evc.EventId, evc.EngineTypeId }); 

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
    .HasKey(evc => new { evc.EventId, evc.VehicleId }); 

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
                entity.HasNoKey();
                entity.ToView("UsersView"); 
            });
            modelBuilder.Entity<EventsView>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("EventsView"); 
            });
            modelBuilder.HasDefaultSchema("21180022");

            modelBuilder.Entity<MFUser>()
       .ToTable("AspNetUsers",  t=>t.UseSqlOutputClause(false));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetAnnotation("SqlServer:ValueGenerationStrategy",
                    SqlServerValueGenerationStrategy.IdentityColumn);
            }

            base.OnModelCreating(modelBuilder);

        }


    }
}
