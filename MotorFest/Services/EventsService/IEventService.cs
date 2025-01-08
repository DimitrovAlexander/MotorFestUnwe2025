using MotorFest.Models.Event;

namespace MotorFest.Services.EventService
{
    public interface IEventService : IBasicCrudService<EventViewModel>
    {
        bool HasVehiclesForEvent(int eventId);
        bool RegisterVehicleForEvent(int eventId, int vehicleId);
        ICollection<EventViewModel> GetAllByUserParticipating(string userId);
    }
}
