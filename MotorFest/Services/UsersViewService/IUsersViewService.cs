using MotorFest.Models.User;

namespace MotorFest.Services.UsersViewService
{
    public interface IUsersViewService
    {
        public ICollection<UsersViewViewModel> GetAll();
        public Task<UsersViewViewModel> GetById(string id);
    }
}
