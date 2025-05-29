using MotorFest.Models.User;

namespace MotorFest.Services.UsersViewService
{
    public interface IUsersViewService :IGet<UsersViewViewModel,string>
    {
        Task<bool> Delete(string id, UsersViewViewModel usersViewViewModel);
    }
}
