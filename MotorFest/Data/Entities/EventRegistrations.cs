namespace MotorFest.Data.Entities
{
    public class EventRegistration
    {

        public int Id { get; set; }
        public int EventId { get; set; }
        public int VehicleId { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Навигационни свойства
        public Event Event { get; set; }
        public Vehicle Vehicle { get; set; }
    }

}

