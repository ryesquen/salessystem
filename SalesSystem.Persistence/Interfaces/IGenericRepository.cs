using System.Linq.Expressions;

namespace SalesSystem.Persistence.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task<T> Add(T entity);
        Task<bool> Edit(T entity);
        Task<bool> Delete(T entity);
        IQueryable<T> Query(Expression<Func<T, bool>>? filter = null);
    }
}