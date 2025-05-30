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

        public async Task<bool> Create(LocationViewModel addressViewModel)
        {
            Location addressEntity = new Location
            {
                Id = addressViewModel.Id,
                Name = addressViewModel.Name,
                City = addressViewModel.City,
                FullAddress = addressViewModel.FullAddress,
                Country = addressViewModel.Country,
                Events = new List<Event>(),
                LastUpdate = DateTime.UtcNow
            };
            await dbContext.Locations.AddAsync(addressEntity);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var address = dbContext.Locations.FirstOrDefault(x => x.Id == id);
            if (address != null)
            {
                address.IsDeleted = true;
                dbContext.Update(address);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public ICollection<LocationViewModel> GetAll()
        {

            return dbContext.Locations
                 .Include(address => address.Events)
                 .ThenInclude(e=>e.EventVehicleCategories)
                 .Include(e=>e.Events)
                 .ThenInclude(e=>e.Organizer)
                 .Where(x => !x.IsDeleted)

                 .Select(address => new LocationViewModel
                 {
                     Id = address.Id,
                     Name = address.Name,
                     City = address.City,
                     FullAddress = address.FullAddress,
                     Country = address.Country,
                     Events = address.Events.Select(eventEntity => new EventViewModel
                     {
                         Id = eventEntity.Id,
                        
                      
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
                    
                    EventDate = eventEntity.EventDate,
                    EntranceFee = eventEntity.EntranceFee,
                    LocationId = eventEntity.LocationId,
                    OrganizerId = eventEntity.OrganizerId,
                    LastUpdate = eventEntity.LastUpdate
                }).ToList(),
                Country = address.Country,
                LastUpdate = address.LastUpdate,
                Name = address.Name
            };
        }

        public async Task<bool> Update(int id, LocationViewModel address)
        {
            var addressEntity = dbContext.Find<Location>(id);
            addressEntity.Country = address.Country;
            addressEntity.Name = address.Name;
            addressEntity.City = address.City;
            addressEntity.FullAddress = address.FullAddress;
            addressEntity.Id = id;
            addressEntity.LastUpdate= DateTime.Now;

            dbContext.Update(addressEntity);
             await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
