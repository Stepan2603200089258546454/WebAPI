using DataContext.Abstractions.Interfaces;
using System.Linq.Expressions;

namespace DataContext.Abstractions.Interfaces
{
    public interface IBaseRepository<T> where T : class, IDBEntity
    {
        Task<int> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IList<T>> ToListAsync(Expression<Func<T, bool>> predicate, int page, int size, CancellationToken cancellationToken = default);
        Task<IList<T>> ToListAsync(int page, int size, CancellationToken cancellationToken = default);
        Task<IList<T>> ToListAsync(CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    }
}