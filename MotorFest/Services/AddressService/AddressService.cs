using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MotorFest.Data;
using MotorFest.Models;

namespace MotorFest.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly MotorFestDbContext dbContext;
        public AddressService(MotorFestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AddressViewModel> Create(AddressViewModel addressViewModel)
        {
            Address addressEntity = new Address
            {
                Id = addressViewModel.Id,
                Name = addressViewModel.Name,
                City = addressViewModel.City,
                FullAddress = addressViewModel.FullAddress,
                Events = new List<Event>(),
                LastUpdate = DateTime.UtcNow
            };
            await dbContext.Addresses.AddAsync(addressEntity);
            await dbContext.SaveChangesAsync();
            return null;
        }

        public async Task<AddressViewModel> Delete(int id)
        {
            var address = dbContext.Addresses.FirstOrDefault(x => x.Id == id);
            if (address != null)
            {
                dbContext.Addresses.Remove(address);
               await dbContext.SaveChangesAsync();
            }
            return null;
        }

        public ICollection<AddressViewModel> GetAll()
        {

            return dbContext.Addresses
                 .Include(address => address.Events)
                 .ThenInclude(e=>e.Category)
                 .Include(e=>e.Events)
                 .ThenInclude(e=>e.Organizer)

                 
                 .Select(address => new AddressViewModel
                 {
                     Id = address.Id,
                     Name = address.Name,
                     City = address.City,
                     FullAddress = address.FullAddress,
                     Municipality = address.Municipality,
                     Events = address.Events.Select(eventEntity => new EventViewModel
                     {
                         Id = eventEntity.Id,
                         CategoryId = eventEntity.CategoryId,
                         Category=new VehicleCategoryViewModel()
                         {
                             Id = eventEntity.CategoryId,
                             Name = eventEntity.Category.Name,

                         },
                         EventDate = eventEntity.EventDate,
                         EntranceFee = eventEntity.EntranceFee,
                         AddressId = eventEntity.AddressId,
                         OrganizerId = eventEntity.OrganizerId,
                         LastUpdate = eventEntity.LastUpdate
                     }).ToList()
                 }).ToList();
        }

        public async Task<AddressViewModel> GetById(int id)
        {
            var address = dbContext.Addresses.FirstOrDefault(x=>x.Id== id);
            return new AddressViewModel
            {
                City = address.City,
                FullAddress = address.FullAddress,
                Id = id,
                Events = address.Events.Select(eventEntity => new EventViewModel
                {
                    Id = eventEntity.Id,
                    CategoryId = eventEntity.CategoryId,
                    EventDate = eventEntity.EventDate,
                    EntranceFee = eventEntity.EntranceFee,
                    AddressId = eventEntity.AddressId,
                    OrganizerId = eventEntity.OrganizerId,
                    LastUpdate = eventEntity.LastUpdate
                }).ToList(),
                Municipality = address.Municipality,
                LastUpdate = address.LastUpdate,
                Name = address.Name
            };
        }

        public async Task<AddressViewModel> Update(int id, AddressViewModel address)
        {
            var addressEntity = dbContext.Find<Address>(id);
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
