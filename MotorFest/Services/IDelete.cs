namespace MotorFest.Services
{
    public interface IDelete<T>
    {
        Task<bool> Delete(int id);

    }
}
