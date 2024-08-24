public interface IBaseRepository<T> where T : class
{
    void Add(T entity);
    void SaveChanges();
}