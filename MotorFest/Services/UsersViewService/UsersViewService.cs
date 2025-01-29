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

        public Task<bool> Update(int id, UsersViewViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
