using MotorFest.Data.Entities;
using MotorFest.Data;
using MotorFest.Services.EventService;
using Microsoft.EntityFrameworkCore;
using MotorFest.Models.Event;
using MotorFest.Models.EventVehicleCategory;

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

        public async Task<EventViewModel> Create(EventViewModel model)
        {
            var newEvent = new Event
            {
                Name = model.Name,
                OrganizerId = model.OrganizerId,
                LocationId = model.LocationId,
                EventDate = model.EventDate,
                EntranceFee = model.EntranceFee
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

            _context.EventVehicleCategories.AddRange(eventCategories);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<EventViewModel> Delete(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity != null)
            {
                _context.Events.Remove(eventEntity);
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public Task<EventViewModel> Update(int id, EventViewModel entity)
        {
            var eventEntity =  _context.Events
             .Include(e => e.EventVehicleCategories)
             .FirstOrDefault(e => e.Id == entity.Id);

            if (eventEntity == null)
            {
                return null;
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

             _context.EventVehicleCategories.AddRangeAsync(newCategories);

             _context.SaveChangesAsync();
            return null;
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
            // Извличаме категорията на превозното средство
            var vehicleCategoryId = _context.Vehicles
                .Where(v => v.Id == vehicleId)
                .Select(v => v.CategoryId)
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

    }
}
