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
            },
            IsCanceled = e.IsCanceled,
            LastUpdate = e.LastUpdate

        })
        .ToList(); // Тук материализираме данните

            // Добавяме информацията за това дали има записани превозни средства
            foreach (var ev in events)
            {
                ev.HasVehicles = HasVehiclesForEvent(ev.Id);
            }

            return events;
        }
        public ICollection<EventViewModel> GetAllByOrganizer(string userId)
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
            },
            IsCanceled = e.IsCanceled,
            LastUpdate = e.LastUpdate
        })
        .Where(x=>x.OrganizerId.Equals(userId))
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
                .ThenInclude(e => e.EngineType)
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
                MinYearOfManufacture=eventEntity.MinYearOfManufacture,
                MaxYearOfManufacture = eventEntity.MaxYearOfManufacture,
                VehicleCategories = eventEntity.EventVehicleCategories.Select(evc => new CheckBoxItem
                {
                    Id = evc.VehicleCategoryId,
                    CategoryName = evc.VehicleCategory.Name,
                    IsChecked = true
                }).ToList(),
                EngineTypes = eventEntity.EventEngineTypes.Select(eet=>new CheckBoxItem
                {
                    Id=eet.EngineTypeId,
                    CategoryName=eet.EngineType.Name,
                    IsChecked = true
                }).ToList(),
                IsCanceled = eventEntity.IsCanceled,
                LastUpdate = eventEntity.LastUpdate
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
                LastUpdate = DateTime.Now
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
            var eventEntity = _context.Events
             .Include(e => e.EventVehicleCategories)
             .Include(e => e.EventEngineTypes)
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
            eventEntity.LastUpdate = DateTime.Now;
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
            await _context.SaveChangesAsync();

            var newEngineTypes = entity.EngineTypes
                .Where(vc => vc.IsChecked)
                .Select(et => new EventEngineType
                {
                    EventId = entity.Id,
                    EngineTypeId = et.Id
                });

            await _context.EventEngineTypes.AddRangeAsync(newEngineTypes);
             _context.Update(eventEntity);
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
                return false; 
            }


            var isCategorySupported = _context.EventVehicleCategories
                .Any(evc => evc.EventId == eventId && evc.VehicleCategoryId == vehicleCategoryId);

            if (!isCategorySupported)
            {
                return false; 
            } 
            var isEngineTypeSupported = _context.EventEngineTypes
                .Any(evc => evc.EventId == eventId && evc.EngineTypeId == vehicleEngineType);

            if (!isEngineTypeSupported)
            {
                return false; 
            } 
            var isYearOfProductionSupported = vehicle.YearOfManufacture > evnt.MinYearOfManufacture && vehicle.YearOfManufacture < evnt.MaxYearOfManufacture;

            if (!isYearOfProductionSupported)
            {
                return false; 
            }

            
            var isVehicleAlreadyRegistered = _context.EventRegistrations
                .Any(er => er.EventId == eventId && er.VehicleId == vehicleId);

            if (isVehicleAlreadyRegistered)
            {
                return false; 
            }

            
            _context.EventRegistrations.Add(new EventRegistration
            {
                EventId = eventId,
                VehicleId = vehicleId,
                RegistrationDate = DateTime.Now
            });

            _context.SaveChanges();

            return true; 
        }
        public ICollection<EventViewModel> GetAllByUserParticipating(string userId)
        {
            var events = _context.EventRegistrations
                .Where(er => _context.Vehicles
                    .Any(v => v.Id == er.VehicleId && v.OwnerId == userId)) 
                .Select(er => er.Event) 
                .Distinct() 
                .Include(e => e.Location) 
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
                .ToList();

            
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

        public async Task Cancel(int id)
        {
            var evnt = _context.Events.FirstOrDefault(e => e.Id == id);
            evnt.IsCanceled= !evnt.IsCanceled;
            await _context.SaveChangesAsync();
        }
    }
}
