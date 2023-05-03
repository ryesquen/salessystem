using Microsoft.EntityFrameworkCore;
using SalesSystem.Persistence.Contexts;
using SalesSystem.Persistence.Interfaces;
using System.Linq.Expressions;

namespace SalesSystem.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbVentaContext _context;
        private readonly DbSet<T> _entity;
        public GenericRepository(DbVentaContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }
        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            try
            {
                T? entity = await _entity.FirstOrDefaultAsync(filter);
                return entity!;
            }
            catch { throw; }
        }
        public async Task<T> Add(T entity)
        {
            try
            {
                await _entity.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw; }
        }
        public async Task<bool> Edit(T entity)
        {
            try
            {
                _entity.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch { throw; }
        }
        public async Task<bool> Delete(T entity)
        {
            try
            {
                _entity.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch { throw; }
        }
        public IQueryable<T> Query(Expression<Func<T, bool>>? filter = null)
        {
            try
            {
                IQueryable<T> query = _entity;
                if (query is not null) query = query.Where(filter!);
                return query!;
            }
            catch { throw; }
        }
    }
}