namespace MotorFest.Services
{
    public interface IUpdate<T>
    {
        Task<bool> Update(int id, T entity);
    }
}
