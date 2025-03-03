using System.ComponentModel.DataAnnotations.Schema;

namespace MotorFest.Data.Entities
{
    public class EventEngineType
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int EngineTypeId { get; set; }
        public EngineType EngineType { get; set; } = null!;
        [Column("21180022_LastUpdate")]

        public DateTime LastUpdate { get; set; } = DateTime.Now;
    }
}
