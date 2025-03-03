using System.ComponentModel.DataAnnotations.Schema;
using MotorFest;

namespace MotorFest.Data.Entities;
public class EventVehicleCategory
{
    public int EventId { get; set; }
    public Event Event { get; set; }

    public int VehicleCategoryId { get; set; }
    public VehicleCategory VehicleCategory { get; set; }
    [Column("21180022_LastUpdate")]

    public DateTime LastUpdate { get; set; } = DateTime.Now;
}
