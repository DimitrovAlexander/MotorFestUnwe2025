using Microsoft.EntityFrameworkCore;
using MotorFest.Data;
using MotorFest.Models.Event;
using MotorFest.Models.Location;
using MotorFest.Models.User;

namespace MotorFest.Services.UsersViewService
{
    public class UsersViewService : IUsersViewService
    {
        private readonly MotorFestDbContext dbContext;

        public UsersViewService(MotorFestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> Delete(string id, UsersViewViewModel usersViewViewModel)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return Task.FromResult(false);
            }
            if (usersViewViewModel.RoleName == "Organizer")
            {
                var events = dbContext.Events.Where(e => e.OrganizerId == user.Id).ToList();
                foreach (var ev in events)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ev.EventLogo);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    foreach (var photo in ev.EventPhotos)
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    dbContext.EventRegistrations.RemoveRange(dbContext.EventRegistrations.Where(er => er.EventId == ev.Id));
                    dbContext.EventVehicleCategories.RemoveRange(dbContext.EventVehicleCategories.Where(evc => evc.EventId == ev.Id));
                    dbContext.EventEngineTypes.RemoveRange(dbContext.EventEngineTypes.Where(eet => eet.EventId == ev.Id));
                    dbContext.Events.Remove(ev);
                }
                dbContext.Users.Remove(user);
            }
            if (usersViewViewModel.RoleName == "Participant" )
            {
                var vehicles = dbContext.Vehicles.Where(v => v.OwnerId == user.Id).ToList();
                foreach (var vehicle in vehicles)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", vehicle.Photo);
                    if (System.IO.File.Exists(filePath)) 
                    {
                        System.IO.File.Delete(filePath); 
                    }
                    dbContext.EventRegistrations.RemoveRange(dbContext.EventRegistrations.Where(er => er.VehicleId == vehicle.Id));
                  
                    dbContext.Vehicles.Remove(vehicle);
                }
                dbContext.Users.Remove(user);
            }
            dbContext.SaveChanges();
            return Task.FromResult(true);
        }

        public ICollection<UsersViewViewModel> GetAll()
        {
            return dbContext.UsersView
            .Select(user => new UsersViewViewModel
            {
                UserId = user.UserId,
                Email = user.Email,
                ParticipatedEventsCount = user.ParticipatedEventsCount,
                OrganizedEventsCount = user.OrganizedEventsCount,
                VehicleCount = user.VehicleCount,
                FullName = user.FullName,

                RoleName = user.RoleName
            }).ToList();
        }

        public async Task<UsersViewViewModel> GetById(string id)
        {
            var user = dbContext.UsersView.FirstOrDefault(x => x.UserId == id);
            var userViewModel = new UsersViewViewModel
            {
                UserId = user.UserId,
                Email = user.Email,

                ParticipatedEventsCount = user.ParticipatedEventsCount,
                OrganizedEventsCount = user.OrganizedEventsCount,
                VehicleCount = user.VehicleCount,
                FullName = user.FullName,

                RoleName = user.RoleName
            };

            return userViewModel;
        }


    }
}
