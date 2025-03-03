using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest.Data.Entities
{
    public class EventRegistration
    {

        public int Id { get; set; }
        public int EventId { get; set; }
        public int VehicleId { get; set; }
        public DateTime RegistrationDate { get; set; }
        [Column("21180022_LastUpdate")]

        public DateTime LastUpdate { get; set; } = DateTime.Now;

        // Навигационни свойства
        public Event Event { get; set; }
        public Vehicle Vehicle { get; set; }
    }

}

