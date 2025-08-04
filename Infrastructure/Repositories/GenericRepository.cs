using System.Linq.Expressions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<T?> Get(Ulid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T?> Find(Expression<Func<T, bool>> ex)
    {
        return  await _dbContext.Set<T>().FirstOrDefaultAsync(ex);
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> ex)
    {
        return  await _dbContext.Set<T>().Where(ex).ToListAsync();
    }

    public async Task Add(T obj)
    {
        await _dbContext.Set<T>().AddAsync(obj);
    }

    public virtual void Delete(T obj)
    {
         _dbContext.Set<T>().Remove(obj);
    }
}