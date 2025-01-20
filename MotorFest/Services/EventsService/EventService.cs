using MotorFest.Data.Entities;
using MotorFest.Data;
using MotorFest.Services.EventService;
using Microsoft.EntityFrameworkCore;
using MotorFest.Models.Event;
using MotorFest.Models.EventVehicleCategory;
using MotorFest.Models.User;
using MotorFest.Models.Vehicle;

namespace MotorFest.Services.EventsService
{
    public class EventService : IEventService
    {
        private readonly MotorFestDbContext _context;

        public EventService(MotorFestDbContext context)
        {
            _context = context;
        }

        public ICollection<EventViewModel> GetAll()
        {
            var events = _context.Events
        .Include(e => e.Location)
        .Include(e => e.EventEngineTypes)
        .Include(e => e.EventVehicleCategories)
        .Select(e => new EventViewModel
        {
            Id = e.Id,
            Name = e.Name,
            OrganizerId = e.OrganizerId,
            LocationId = e.LocationId,
            EventDate = e.EventDate,
            EntranceFee = e.EntranceFee,
            VehicleCategories = e.EventVehicleCategories.Select(evc => new CheckBoxItem
            {
                Id = evc.VehicleCategoryId,
                IsChecked = true
            }).ToList(),
            Location = new Models.Location.LocationViewModel()
            {
                Id = e.Location.Id,
                Name = e.Location.Name,
                Municipality = e.Location.Municipality,
                City = e.Location.City,
                FullAddress = e.Location.FullAddress,
                LastUpdate = e.Location.LastUpdate,
            }
        })
        .ToList(); // Тук материализираме данните

            // Добавяме информацията за това дали има записани превозни средства
            foreach (var ev in events)
            {
                ev.HasVehicles = HasVehiclesForEvent(ev.Id);
            }

            return events;
        }

        public async Task<EventViewModel> GetById(int id)
        {
            var eventEntity = await _context.Events
                .Include(e => e.EventEngineTypes)
                .Include(e => e.EventVehicleCategories)
                .ThenInclude(ec => ec.VehicleCategory)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventEntity == null) return null;

            return new EventViewModel
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                OrganizerId = eventEntity.OrganizerId,
                LocationId = eventEntity.LocationId,
                EventDate = eventEntity.EventDate,
                EntranceFee = eventEntity.EntranceFee,
                VehicleCategories = eventEntity.EventVehicleCategories.Select(evc => new CheckBoxItem
                {
                    Id = evc.VehicleCategoryId,
                    CategoryName = evc.VehicleCategory.Name,
                    IsChecked = true
                }).ToList()
            };
        }

        public async Task<bool> Create(EventViewModel model)
        {
            var newEvent = new Event
            {
                Name = model.Name,
                OrganizerId = model.OrganizerId,
                LocationId = model.LocationId,
                EventDate = model.EventDate,
                EntranceFee = model.EntranceFee,
                MinYearOfManufacture = model.MinYearOfManufacture,
                MaxYearOfManufacture = model.MaxYearOfManufacture,
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            var eventCategories = model.VehicleCategories
                .Where(vc => vc.IsChecked)
                .Select(vc => new EventVehicleCategory
                {
                    EventId = newEvent.Id,
                    VehicleCategoryId = vc.Id
                });
            var eventEngineTypes = model.EngineTypes
            .Where(et => et.IsChecked)
            .Select(et => new EventEngineType
            {
                EventId= newEvent.Id,
                EngineTypeId = et.Id
            });

            _context.EventVehicleCategories.AddRange(eventCategories);
            _context.EventEngineTypes.AddRange(eventEngineTypes);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity != null)
            {
                _context.Events.Remove(eventEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Update(int id, EventViewModel entity)
        {
            var eventEntity =  _context.Events
             .Include(e => e.EventVehicleCategories)
             .FirstOrDefault(e => e.Id == entity.Id);

            if (eventEntity == null)
            {
                return false;
            }
            eventEntity.Name = entity.Name;
            
            eventEntity.LocationId = entity.LocationId;
            eventEntity.EventDate = entity.EventDate;
            eventEntity.EntranceFee = entity.EntranceFee;

            var existingCategories = eventEntity.EventVehicleCategories;
            _context.EventVehicleCategories.RemoveRange(existingCategories);

            var newCategories = entity.VehicleCategories
                .Where(vc => vc.IsChecked)
                .Select(vc => new EventVehicleCategory
                {
                    EventId = entity.Id,
                    VehicleCategoryId = vc.Id
                });
            var existingEngineTypes = eventEntity.EventEngineTypes;
            _context.EventEngineTypes.RemoveRange(existingEngineTypes);

            var newEngineTypes = entity.EngineTypes
                .Where(vc => vc.IsChecked)
                .Select(et => new EventEngineType
                {
                    EventId = entity.Id,
                    EngineTypeId = et.Id
                });

            await _context.EventEngineTypes.AddRangeAsync(newEngineTypes);

            await _context.SaveChangesAsync();
            return true;
        }
        public bool HasVehiclesForEvent(int eventId)
        {
            return _context.EventRegistrations
                .Include(v => v.Vehicle)
                .ThenInclude(vc => vc.Owner)
                .Any(vr=>vr.EventId==eventId);
        }
        public bool RegisterVehicleForEvent(int eventId, int vehicleId)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id==vehicleId);
            var evnt = _context.Events.FirstOrDefault(e => e.Id==eventId);
            // Извличаме категорията на превозното средство
            var vehicleCategoryId = _context.Vehicles
                .Where(v => v.Id == vehicleId)
                .Select(v => v.CategoryId)
                .FirstOrDefault();
            var vehicleEngineType = _context.Vehicles
                .Where(v => v.Id == vehicleId)
                .Select(v => v.EngineTypeId)
                .FirstOrDefault();
            if (vehicleCategoryId == 0)
            {
                return false; // Превозното средство не съществува
            }

            // Проверяваме дали събитието поддържа тази категория
            var isCategorySupported = _context.EventVehicleCategories
                .Any(evc => evc.EventId == eventId && evc.VehicleCategoryId == vehicleCategoryId);

            if (!isCategorySupported)
            {
                return false; // Категорията на превозното средство не е свързана със събитието
            } // Проверяваме дали събитието поддържа тази категория
            var isEngineTypeSupported = _context.EventEngineTypes
                .Any(evc => evc.EventId == eventId && evc.EngineTypeId == vehicleEngineType);

            if (!isEngineTypeSupported)
            {
                return false; // Категорията на превозното средство не е свързана със събитието
            } // Проверяваме дали събитието поддържа тази категория
            var isYearOfProductionSupported = vehicle.YearOfManufacture > evnt.MinYearOfManufacture && vehicle.YearOfManufacture < evnt.MaxYearOfManufacture;

            if (!isYearOfProductionSupported)
            {
                return false; // Категорията на превозното средство не е свързана със събитието
            }

            // Проверяваме дали превозното средство вече е записано за това събитие
            var isVehicleAlreadyRegistered = _context.EventRegistrations
                .Any(er => er.EventId == eventId && er.VehicleId == vehicleId);

            if (isVehicleAlreadyRegistered)
            {
                return false; // Превозното средство вече е записано
            }

            // Създаваме нов запис за регистрация на превозното средство към събитието
            _context.EventRegistrations.Add(new EventRegistration
            {
                EventId = eventId,
                VehicleId = vehicleId,
                RegistrationDate = DateTime.Now
            });

            _context.SaveChanges();

            return true; // Успешна регистрация
        }
        public ICollection<EventViewModel> GetAllByUserParticipating(string userId)
        {
            var events = _context.EventRegistrations
                .Where(er => _context.Vehicles
                    .Any(v => v.Id == er.VehicleId && v.OwnerId == userId)) // Филтрираме регистрациите на събития по превозни средства на потребителя
                .Select(er => er.Event) // Връзката към събитие
                .Distinct() // Премахваме дублираните записи, ако едно събитие има повече от една регистрация за дадения потребител
                .Include(e => e.Location) // Зареждаме локацията
                .Include(e => e.EventVehicleCategories) // Зареждаме категориите на събитията
                .Select(e => new EventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    OrganizerId = e.OrganizerId,
                    LocationId = e.LocationId,
                    EventDate = e.EventDate,
                    EntranceFee = e.EntranceFee,
                    VehicleCategories = e.EventVehicleCategories.Select(evc => new CheckBoxItem
                    {
                        Id = evc.VehicleCategoryId,
                        IsChecked = true
                    }).ToList(),
                    Location = new Models.Location.LocationViewModel()
                    {
                        Id = e.Location.Id,
                        Name = e.Location.Name,
                        Municipality = e.Location.Municipality,
                        City = e.Location.City,
                        FullAddress = e.Location.FullAddress,
                        LastUpdate = e.Location.LastUpdate,
                    }
                })
                .ToList();

            // Добавяме информацията за това дали има записани превозни средства
            foreach (var ev in events)
            {
                ev.HasVehicles = HasVehiclesForEvent(ev.Id);
            }

            return events;
        }
        public ICollection<VehicleViewModel> GetRegisteredVehiclesForEvent(int eventId)
        {
            return _context.EventRegistrations
                .Where(er => er.EventId == eventId)
                .Include(er => er.Vehicle)
                .ThenInclude(v => v.Owner)
                .Select(er => new VehicleViewModel
                {
                    Id = er.Vehicle.Id,
                    Manufacturer = er.Vehicle.Manufacturer,
                    Model = er.Vehicle.Model,
                    YearOfManufacture = er.Vehicle.YearOfManufacture,
                    Owner = new UserViewModel
                    {
                        Firstname = er.Vehicle.Owner.Firstname,
                        Lastname = er.Vehicle.Owner.Lastname
                    }
                }).ToList();
        }
        public bool IsUserRegisteredForEvent(string userId, int eventId)
        {
            return _context.EventRegistrations.Any(er => er.EventId == eventId && er.Vehicle.OwnerId == userId);
        }

    }
}
