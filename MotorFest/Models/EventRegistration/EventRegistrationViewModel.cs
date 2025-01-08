using MotorFest.Models.Event;
using MotorFest.Models.Vehicle;

namespace MotorFest.Models.EventRegistration
{
    public class EventRegistrationViewModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int VehicleId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public EventViewModel Event { get; set; }
        public VehicleViewModel Vehicle { get; set; }
    }
}
