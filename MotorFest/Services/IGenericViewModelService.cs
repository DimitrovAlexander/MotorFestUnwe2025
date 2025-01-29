using MotorFest.Models.User;

namespace MotorFest.Services
{
    public interface IGenericViewModelService<T, K>
    {
        ICollection<T> GetAll();
        Task<UsersViewViewModel> GetById(K id);
    }
}
