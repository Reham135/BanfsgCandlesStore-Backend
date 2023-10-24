
using Microsoft.EntityFrameworkCore;

namespace Banfsg.DAL;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    private readonly BanfsgContext _context;

    public GenericRepo(BanfsgContext context)
    {
        _context = context;
    }
    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public T? GetBYId(int id)
    {
        return _context.Set<T>().Find(id);
    }
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
    public void Update(T entity)
    {
 
    }

    public void Delete(T entity)
    {
       _context.Set<T>().Remove(entity);
    }

    

    
}
