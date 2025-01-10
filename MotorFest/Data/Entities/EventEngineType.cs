namespace MotorFest.Data.Entities
{
    public class EventEngineType
    {
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public int EngineTypeId { get; set; }
        public EngineType EngineType { get; set; } = null!;
    }
}
