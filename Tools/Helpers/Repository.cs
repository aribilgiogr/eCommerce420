using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tools.Helpers
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _set;

        protected Repository(DbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>>? expression = null) => await _set.AnyAsync(expression ?? (x => true));

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? expression = null) => await _set.CountAsync(expression ?? (x => true));

        public virtual async Task CreateAsync(T entity) => await _set.AddAsync(entity);

        public virtual async Task CreateManyAsync(IEnumerable<T> entities) => await _set.AddRangeAsync(entities);

        public virtual async Task DeleteAsync(object key)
        {
            var entity = await ReadAsync(key);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        public virtual async Task DeleteAsync(T entity) => await Task.Run(() => _set.Remove(entity));

        public virtual async Task DeleteManyAsync(IEnumerable<T> entities) => await Task.Run(() => _set.RemoveRange(entities));

        public virtual async Task DeleteManyAsync(Expression<Func<T, bool>>? expression = null)
        {
            var entities = await ReadManyAsync(expression);
            await DeleteManyAsync(entities);
        }

        public virtual async Task<T?> ReadAsync(object key) => await _set.FindAsync(key);

        public virtual async Task<IEnumerable<T>> ReadManyAsync(Expression<Func<T, bool>>? expression = null, params string[] includes)
        {
            var data = _set.Where(expression ?? (x => true));
            foreach (var include in includes)
            {
                data = data.Include(include);
            }
            return await data.ToListAsync();
        }

        public virtual async Task UpdateAsync(T entity) => await Task.Run(() => _set.Update(entity));

        public virtual async Task UpdateManyAsync(IEnumerable<T> entities) => await Task.Run(() => _set.UpdateRange(entities));
    }
}
