namespace MotorFest.Services
{
    public interface IBasicCrudService<T>
    {
      Task<bool> Create(T entity);
        Task<bool>  Update(int id, T entity);
        Task<bool> Delete(int id);
        Task<T> GetById(int id);
        ICollection<T> GetAll();
    }
}
