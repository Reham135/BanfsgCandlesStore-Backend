namespace Banfsg.DAL;

public interface IGenericRepo<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetBYId(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);

}
