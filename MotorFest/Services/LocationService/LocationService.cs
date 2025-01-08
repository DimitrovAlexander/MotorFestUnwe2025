using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MotorFest.Data;
using MotorFest.Data.Entities;
using MotorFest.Models.Event;
using MotorFest.Models.Location;

namespace MotorFest.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly MotorFestDbContext dbContext;
        public LocationService(MotorFestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<LocationViewModel> Create(LocationViewModel addressViewModel)
        {
            Location addressEntity = new Location
            {
                Id = addressViewModel.Id,
                Name = addressViewModel.Name,
                City = addressViewModel.City,
                FullAddress = addressViewModel.FullAddress,
                Municipality = addressViewModel.Municipality,
                Events = new List<Event>(),
                LastUpdate = DateTime.UtcNow
            };
            await dbContext.Locations.AddAsync(addressEntity);
            await dbContext.SaveChangesAsync();
            return null;
        }

        public async Task<LocationViewModel> Delete(int id)
        {
            var address = dbContext.Locations.FirstOrDefault(x => x.Id == id);
            if (address != null)
            {
                dbContext.Locations.Remove(address);
               await dbContext.SaveChangesAsync();
            }
            return null;
        }

        public ICollection<LocationViewModel> GetAll()
        {

            return dbContext.Locations
                 .Include(address => address.Events)
                 .ThenInclude(e=>e.EventVehicleCategories)
                 .Include(e=>e.Events)
                 .ThenInclude(e=>e.Organizer)

                 
                 .Select(address => new LocationViewModel
                 {
                     Id = address.Id,
                     Name = address.Name,
                     City = address.City,
                     FullAddress = address.FullAddress,
                     Municipality = address.Municipality,
                     Events = address.Events.Select(eventEntity => new EventViewModel
                     {
                         Id = eventEntity.Id,
                         //VehicleCategories = eventEntity.EventVehicleCategories.Select(vehicleCategory=>new EventVehicleCategory
                         //{
                         //    EventId=vehicleCategory.EventId,
                         //    VehicleCategoryId=vehicleCategory.VehicleCategoryId
                         //}).ToList(),
                        
                         EventDate = eventEntity.EventDate,
                         EntranceFee = eventEntity.EntranceFee,
                         LocationId = eventEntity.LocationId,
                         OrganizerId = eventEntity.OrganizerId,
                         LastUpdate = eventEntity.LastUpdate
                     }).ToList()
                 }).ToList();
        }

        public async Task<LocationViewModel> GetById(int id)
        {
            var address = dbContext.Locations.FirstOrDefault(x=>x.Id== id);
            return new LocationViewModel
            {
                City = address.City,
                FullAddress = address.FullAddress,
                Id = id,
                Events = address.Events.Select(eventEntity => new EventViewModel
                {
                    Id = eventEntity.Id,
                    //VehicleCategories = eventEntity.EventVehicleCategories.Select(vehicleCategory => new EventVehicleCategory
                    //{
                    //    EventId = vehicleCategory.EventId,
                    //    VehicleCategoryId = vehicleCategory.VehicleCategoryId
                    //}).ToList(),
                    EventDate = eventEntity.EventDate,
                    EntranceFee = eventEntity.EntranceFee,
                    LocationId = eventEntity.LocationId,
                    OrganizerId = eventEntity.OrganizerId,
                    LastUpdate = eventEntity.LastUpdate
                }).ToList(),
                Municipality = address.Municipality,
                LastUpdate = address.LastUpdate,
                Name = address.Name
            };
        }

        public async Task<LocationViewModel> Update(int id, LocationViewModel address)
        {
            var addressEntity = dbContext.Find<Location>(id);
            addressEntity.Municipality = address.Municipality;
            addressEntity.Name = address.Name;
            addressEntity.City = address.City;
            addressEntity.FullAddress = address.FullAddress;
            addressEntity.Id = id;
            addressEntity.LastUpdate= DateTime.Now;

            dbContext.Update(addressEntity);
             await dbContext.SaveChangesAsync();
            return address;
        }
    }
}
