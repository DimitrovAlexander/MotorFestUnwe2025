using MotorFest.Models;

namespace MotorFest.Services.EventService
{
    public interface IEventService : IBasicCrudService<EventViewModel>
    {
        Task AddVehicleCategoriesToEvent(int eventId, List<int> categoryIds);
        Task<ICollection<VehicleCategoryViewModel>> GetVehicleCategoriesByEvent(int eventId);
    }
}
