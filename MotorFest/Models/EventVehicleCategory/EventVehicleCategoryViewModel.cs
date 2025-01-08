using MotorFest.Models.Event;
using MotorFest.Models.VehicleCategories;

namespace MotorFest.Models.EventVehicleCategory
{
    public class EventVehicleCategoryViewModel
    {
        public int EventId { get; set; }
        public EventViewModel Event { get; set; }

        public int VehicleCategoryId { get; set; }
        public VehicleCategoryViewModel VehicleCategory { get; set; }
    }
}
