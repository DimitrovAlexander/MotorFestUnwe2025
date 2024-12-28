using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest;

public partial class VehicleCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    [Column("21180022_LastUpdate")]

    public DateTime LastUpdate { get; set; } = DateTime.Now;
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
