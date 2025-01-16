using MotorFest.Models.Vehicle;

namespace MotorFest.Services.VehiclesService
{
    public interface IVehicleService :IBasicCrudService<VehicleViewModel>
    {
        ICollection<VehicleViewModel> GetByUserId(string userId);
        bool IsParticipatingInFutureEvents(int id);
    }
}
