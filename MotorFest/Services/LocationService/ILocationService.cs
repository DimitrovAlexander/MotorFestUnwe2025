using MotorFest.Models.Event;
using MotorFest.Models.Location;

namespace MotorFest.Services.LocationService
{
    public interface ILocationService : ICreate<LocationViewModel>, IUpdate<LocationViewModel>, IGet<LocationViewModel, int>, IDelete<LocationViewModel>
    {

    }
}
