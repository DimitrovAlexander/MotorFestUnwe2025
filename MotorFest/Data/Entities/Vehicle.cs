using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MotorFest.Data.Entities;

namespace MotorFest;

public partial class Vehicle
{
    public int Id { get; set; }

    public string OwnerId { get; set; }

    public int CategoryId { get; set; }

    public int EngineTypeId { get; set; }

    public string Manufacturer { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int YearOfManufacture { get; set; }

    public string? Photo { get; set; }
    [Column("21180022_LastUpdate")]

    public DateTime LastUpdate { get; set; } =DateTime.Now;

    public virtual VehicleCategory Category { get; set; } = null!;

    public virtual EngineType EngineType { get; set; } = null!;

    public virtual MFUser Owner { get; set; } = null!;
}
