using MotorFest;

namespace MotorFest.Data.Entities;
public class EventVehicleCategory
{
    public int EventId { get; set; }
    public Event Event { get; set; }

    public int VehicleCategoryId { get; set; }
    public VehicleCategory VehicleCategory { get; set; }
}
