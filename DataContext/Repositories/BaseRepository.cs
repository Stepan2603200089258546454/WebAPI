using DataContext.Abstractions.Interfaces;
using DataContext.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DataContext.Repositories
{
    internal abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IDBEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _table;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        protected abstract IQueryable<T> Include(IQueryable<T> query);
        protected IQueryable<T> Page(IQueryable<T> query, int page, int size)
        {
            page = Math.Max(page, 1);
            size = Math.Max(size, 1);

            return query
                .Skip((page - 1) * size)
                .Take(size);
        }
        protected IQueryable<T> Page(int page, int size)
        {
            page = Math.Max(page, 1);
            size = Math.Max(size, 1);

            return _table
                .Skip((page - 1) * size)
                .Take(size);
        }
        protected virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _table.Where(predicate);
            query = Include(query);
            return query;
        }
        protected virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate, int page, int size)
        {
            IQueryable<T> query = Where(predicate);
            query = Page(query, page, size);
            return query;
        }

        public virtual async Task<IList<T>> ToListAsync(int page, int size, CancellationToken cancellationToken = default)
        {
            return await Page(page, size).ToListAsync(cancellationToken);
        }
        public virtual async Task<IList<T>> ToListAsync(CancellationToken cancellationToken = default)
        {
            return await _table.ToListAsync(cancellationToken);
        }
        public virtual async Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate, int page, int size, CancellationToken cancellationToken = default)
        {
            return await Where(predicate, page, size).ToListAsync(cancellationToken);
        }
        public virtual async Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Where(predicate).ToListAsync(cancellationToken);
        }
        public virtual async Task<int> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _table.AddAsync(entity, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            EntityEntry<T> updater = _table.Update(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            // не поддерживается в БД в памяти
            //return await _table
            //    .Where(predicate)
            //    .ExecuteDeleteAsync(cancellationToken);

            List<T> entityDeleted = await _table.Where(predicate).ToListAsync(cancellationToken);
            _table.RemoveRange(entityDeleted);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
