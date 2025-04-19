namespace MotorFest.Services
{
    public interface IGet<T,K>
    {
        Task<T> GetById(K id);
        ICollection<T> GetAll();
    }
}
