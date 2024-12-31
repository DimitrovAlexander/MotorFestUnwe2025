namespace MotorFest.Services
{
    public interface IBasicCrudService<T>
    {
      Task<T> Create(T entity);
        Task<T>  Update(int id, T entity);
        Task<T> Delete(int id);
        Task<T> GetById(int id);
        ICollection<T> GetAll();
    }
}
