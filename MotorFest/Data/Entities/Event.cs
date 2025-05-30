using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MotorFest.Data.Entities;

namespace MotorFest;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string OrganizerId { get; set; }

    public int LocationId { get; set; }
    public DateTime EventDate { get; set; }

    public string Description { get; set; }
    public decimal EntranceFee { get; set; }
    [Column("21180022_LastUpdate")]

    public DateTime LastUpdate { get; set; } = DateTime.Now;

    public virtual Location Location { get; set; } = null!;

    public virtual MFUser Organizer { get; set; } = null!;
    public int? MinYearOfManufacture { get; set; }
    public int? MaxYearOfManufacture { get; set; }
    public bool IsCanceled { get; set; } = false;
    public string EventLogo { get; set; }
    public ICollection<string> EventPhotos { get; set; } = new List<string>();
    public virtual ICollection<EventEngineType> EventEngineTypes { get; set; } = new List<EventEngineType>();

    public virtual ICollection<EventVehicleCategory> EventVehicleCategories { get; set; } = new List<EventVehicleCategory>();
    public virtual ICollection<EventRegistration> EventRegistration { get; set; } = new HashSet<EventRegistration>();

   
}
