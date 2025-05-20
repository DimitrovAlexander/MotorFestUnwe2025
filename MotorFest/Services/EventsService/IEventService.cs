using MotorFest.Models.Event;
using MotorFest.Models.Vehicle;

namespace MotorFest.Services.EventService
{
    public interface IEventService : ICreate<EventViewModel>, IUpdate<EventViewModel>,IGet<EventViewModel,int>, IDelete<EventViewModel>
    {
        bool HasVehiclesForEvent(int eventId);
        bool RegisterVehicleForEvent(int eventId, int vehicleId);
        ICollection<EventViewModel> GetAllByUserParticipating(string userId);
        ICollection<VehicleViewModel> GetRegisteredVehiclesForEvent(int eventId);
        bool IsUserRegisteredForEvent(string userId, int eventId);
        ICollection<EventViewModel> GetAllByOrganizer(string userId);
        ICollection<EventViewModel> GetUpcomingEvent();
        Task Cancel(int id);
        string GenerateCsvForUserEvents(string userId);
        bool CheckVehicleCompatibility(int eventId, int vehicleId);
    }
}
