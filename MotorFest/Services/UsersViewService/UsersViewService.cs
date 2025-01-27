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
                EventCount = user.EventCount,
                VehicleCount = user.VehicleCount,
                FullName = user.FullName,
                LastUpdate = user.LastUpdate,
                RoleName = user.RoleName
            }).ToList();
        }

        public Task<UsersViewViewModel> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int id, UsersViewViewModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
