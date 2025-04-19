using MotorFest.Models.Vehicle;

namespace MotorFest.Services.VehiclesService
{
    public interface IVehicleService : ICreate<VehicleViewModel>, IUpdate<VehicleViewModel>, IGet<VehicleViewModel, int>, IDelete<VehicleViewModel>
    {
        ICollection<VehicleViewModel> GetByUserId(string userId);
        bool IsParticipatingInFutureEvents(int id);
    }
}
