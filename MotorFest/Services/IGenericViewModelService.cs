using MotorFest.Models.User;

namespace MotorFest.Services
{
    public interface IGenericViewModelService<T, K>
    {
        ICollection<T> GetAll();
        Task<T> GetById(K id);
    }
}
