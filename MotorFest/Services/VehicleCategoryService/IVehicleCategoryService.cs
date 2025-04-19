using MotorFest.Models.VehicleCategories;

namespace MotorFest.Services.VehicleCategoryService
{
    public interface IVehicleCategoryService :ICreate<VehicleCategoryViewModel>, IUpdate<VehicleCategoryViewModel>, IGet<VehicleCategoryViewModel, int>, IDelete<VehicleCategoryViewModel>
    {
    }
}
