using MotorFest.Data;
using MotorFest.Models;

namespace MotorFest.Services.VehicleCategoryService
{
    public class VehicleCategoryService :IVehicleCategoryService
    {
         private readonly MotorFestDbContext dbContext;
        public VehicleCategoryService(MotorFestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<VehicleCategoryViewModel> Create(VehicleCategoryViewModel vehicleCategoryViewModel)
        {
            VehicleCategory vehicleCategoryEntity = new VehicleCategory
            {
                Id = vehicleCategoryViewModel.Id,
                Name = vehicleCategoryViewModel.Name,

                LastUpdate = DateTime.UtcNow
            };
            await dbContext.VehicleCategories.AddAsync(vehicleCategoryEntity);
            await dbContext.SaveChangesAsync();
            return null;
        }

        public async Task<VehicleCategoryViewModel> Delete(int id)
        {
            var vehicleCategory = dbContext.VehicleCategories.FirstOrDefault(x => x.Id == id);
            if (vehicleCategory != null)
            {
                dbContext.VehicleCategories.Remove(vehicleCategory);
                await dbContext.SaveChangesAsync();
            }
            return null;
        }

        public ICollection<VehicleCategoryViewModel> GetAll()
        {

            return dbContext.VehicleCategories

                 .Select(vehicleCategory => new VehicleCategoryViewModel
                 {
                     Id = vehicleCategory.Id,
                     Name = vehicleCategory.Name,

                 }).ToList();
        }

        public async Task<VehicleCategoryViewModel> GetById(int id)
        {
            var vehicleCategory = dbContext.VehicleCategories.FirstOrDefault(x => x.Id == id);
            return new VehicleCategoryViewModel
            {

                Id = id,

                LastUpdate = vehicleCategory.LastUpdate,
                Name = vehicleCategory.Name
            };
        }

        public async Task<VehicleCategoryViewModel> Update(int id, VehicleCategoryViewModel vehicleCategory)
        {
            var vehicleCategoryEntity = dbContext.Find<VehicleCategory>(id);

            vehicleCategoryEntity.Id = id;
            vehicleCategoryEntity.Name = vehicleCategory.Name;
            vehicleCategoryEntity.LastUpdate = DateTime.Now;

            dbContext.Update(vehicleCategoryEntity);
            await dbContext.SaveChangesAsync();
            return vehicleCategory;
        }
    }
}
