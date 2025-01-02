using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MotorFest.Data.Entities;

namespace MotorFest;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; }
    public int OrganizerId { get; set; }

    public int LocationId { get; set; }

    public DateTime EventDate { get; set; }


    public decimal EntranceFee { get; set; }
    [Column("21180022_LastUpdate")]

    public DateTime LastUpdate { get; set; } =DateTime.Now;

    public virtual Location Location { get; set; } = null!;

    public virtual MFUser Organizer { get; set; } = null!;
    public ICollection<EventVehicleCategory> EventVehicleCategories { get; set; } = new List<EventVehicleCategory>();
}
