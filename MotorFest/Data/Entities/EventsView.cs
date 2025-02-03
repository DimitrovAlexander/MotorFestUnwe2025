using MotorFest.Models.User;

namespace MotorFest.Data.Entities
{
    public class EventsView
    {
        public  int EventId { get; set; }
        public string EventName { get; set; }
        public  string OrganizerId { get; set; }
        public string OrganizerFullName { get; set; }

        public string LocationName { get; set; }
        public DateTime EventDate { get; set; }
        public decimal EntranceFee { get; set; }
        public int RegisteredParticipantsCount { get; set; }
        public decimal ExpectedRevenue { get; set; }
        public int AllowedEngineTypesCount { get; set; }
        public int AllowedVehicleCategoriesCount { get; set; }
    }
}
