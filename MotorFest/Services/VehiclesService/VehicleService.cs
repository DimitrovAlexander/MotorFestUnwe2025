using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MotorFest.Data;
using MotorFest.Models.EngineType;
using MotorFest.Models.User;
using MotorFest.Models.Vehicle;
using MotorFest.Models.VehicleCategories;

namespace MotorFest.Services.VehiclesService
{
    public class VehicleService : IVehicleService
    {
        private readonly MotorFestDbContext dbContext;

        public VehicleService(MotorFestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> Create(VehicleViewModel VehicleViewModel)
        {
            Vehicle vehicleEntity = new Vehicle
            {
                Id = VehicleViewModel.Id,
                CategoryId = VehicleViewModel.CategoryId,
                EngineTypeId = VehicleViewModel.EngineTypeId,
                Manufacturer = VehicleViewModel.Manufacturer,
                Model = VehicleViewModel.Model,
                OwnerId = VehicleViewModel.OwnerId,
                Photo = VehicleViewModel.Photo,
                YearOfManufacture = VehicleViewModel.YearOfManufacture,
                LastUpdate = DateTime.Now
            };
            await dbContext.Vehicles.AddAsync(vehicleEntity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var vehicle = dbContext.Vehicles.FirstOrDefault(x => x.Id == id);
            if (vehicle != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", vehicle.Photo);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                var registrations = dbContext.EventRegistrations.Where(x => x.VehicleId == vehicle.Id);
                dbContext.EventRegistrations.RemoveRange(registrations);
                dbContext.Vehicles.Remove(vehicle);
                await dbContext.SaveChangesAsync();
            return true;
            }
            return false;
        }

        public ICollection<VehicleViewModel> GetAll()
        {

            return dbContext.Vehicles
                 .Include(x => x.EngineType)
                 .Include(x => x.Owner)
                 .Include(x => x.Category)

                 .Select(vehicle => new VehicleViewModel
                 {
                     Id = vehicle.Id,
                     Model = vehicle.Model,
                     EngineType = new EngineTypeViewModel()
                     {
                         Id = vehicle.EngineTypeId,
                         Name = vehicle.EngineType.Name

                     },
                     Manufacturer = vehicle.Manufacturer,
                     Owner = new UserViewModel()
                     {
                         Id = vehicle.OwnerId,
                         Firstname = vehicle.Owner.Firstname,
                         Lastname = vehicle.Owner.Lastname,
                         Identifier = vehicle.Owner.Identifier,

                     },
                     EngineTypeId = vehicle.EngineTypeId,
                     OwnerId = vehicle.OwnerId,
                     CategoryId = vehicle.CategoryId,
                     Photo = vehicle.Photo,
                     YearOfManufacture = vehicle.YearOfManufacture,
                     LastUpdate = vehicle.LastUpdate,
                     Category = new VehicleCategoryViewModel()
                     {
                         Id = vehicle.CategoryId,
                         Name = vehicle.Category.Name,
                         LastUpdate = vehicle.Category.LastUpdate,
                     }


                 }).ToList();
        }


        public ICollection<VehicleViewModel> GetByUserId(string userId)
        {

            return dbContext.Vehicles
                 .Include(x => x.EngineType)
                 .Include(x => x.Owner)
                 .Include(x => x.Category)
                 .Where(x=>x.OwnerId== userId)
                 .Select(vehicle => new VehicleViewModel
                 {
                     Id = vehicle.Id,
                     Model = vehicle.Model,
                     EngineType = new EngineTypeViewModel()
                     {
                         Id = vehicle.EngineTypeId,
                         Name = vehicle.EngineType.Name

                     },
                     Manufacturer = vehicle.Manufacturer,
                     Owner = new UserViewModel()
                     {
                         Id = vehicle.OwnerId,
                         Firstname = vehicle.Owner.Firstname,
                         Lastname = vehicle.Owner.Lastname,
                         Identifier = vehicle.Owner.Identifier,

                     },
                     EngineTypeId = vehicle.EngineTypeId,
                     OwnerId = vehicle.OwnerId,
                     CategoryId = vehicle.CategoryId,
                     Photo = vehicle.Photo,
                     YearOfManufacture = vehicle.YearOfManufacture,
                     LastUpdate = vehicle.LastUpdate,
                     Category = new VehicleCategoryViewModel()
                     {
                         Id = vehicle.CategoryId,
                         Name = vehicle.Category.Name,
                         LastUpdate = vehicle.Category.LastUpdate,
                     }


                 }).ToList();
        }

        public async Task<VehicleViewModel> GetById(int id)
        {
            var vehicle = dbContext.Vehicles
                .Include(v=>v.Owner)
                .Include(v=>v.Category)
                .Include(v=>v.EngineType)
                .FirstOrDefault(x => x.Id == id);
            return new VehicleViewModel
            {
                Id = vehicle.Id,
                Model = vehicle.Model,
                EngineType = new EngineTypeViewModel()
                {
                    Id = vehicle.EngineTypeId,
                    Name = vehicle.EngineType.Name

                },
                Manufacturer = vehicle.Manufacturer,
                Owner = new UserViewModel()
                {
                    Id = vehicle.OwnerId,
                    Firstname = vehicle.Owner.Firstname,
                    Lastname = vehicle.Owner.Lastname,
                    Identifier = vehicle.Owner.Identifier,

                },
                EngineTypeId = vehicle.EngineTypeId,
                OwnerId = vehicle.OwnerId,
                CategoryId = vehicle.CategoryId,
                Photo = vehicle.Photo,
                YearOfManufacture = vehicle.YearOfManufacture,
                LastUpdate = vehicle.LastUpdate,
                Category = new VehicleCategoryViewModel()
                {
                    Id = vehicle.CategoryId,
                    Name = vehicle.Category.Name,
                    LastUpdate = vehicle.Category.LastUpdate,
                }


            };
        }

        public async Task<bool> Update(int id, VehicleViewModel vehicle)
        {
            var vehicleEntity = dbContext.Find<Vehicle>(id);
            vehicleEntity.YearOfManufacture = vehicle.YearOfManufacture;
            
            vehicleEntity.CategoryId=vehicle.CategoryId;
            vehicleEntity.EngineTypeId=vehicle.EngineTypeId;
            
            vehicleEntity.Model=vehicle.Model;
            vehicleEntity.Manufacturer = vehicle.Manufacturer;

            vehicleEntity.LastUpdate = DateTime.Now;

            dbContext.Update(vehicleEntity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public bool IsParticipatingInFutureEvents(int id)
        {
            return dbContext.EventRegistrations
                .Include(e=>e.Event)
                .Include(v => v.Vehicle)
            .ThenInclude(vc => vc.Owner)
                .Any(vr => vr.VehicleId == id&&vr.Event.EventDate>DateTime.Now);
        }
    }
}
