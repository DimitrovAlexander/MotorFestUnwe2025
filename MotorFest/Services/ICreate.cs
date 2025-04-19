namespace MotorFest.Services
{
    public interface ICreate<T>
    {
        Task<bool> Create(T entity);
    }
}
