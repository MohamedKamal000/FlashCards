using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<T?> Get(string id);

    public Task AddRange(IEnumerable<T> objs);
    
    public Task<T?> Find(Expression<Func<T,bool>> ex);

    public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> ex);

    public Task Add(T obj); 

    public void Delete(T obj);
}