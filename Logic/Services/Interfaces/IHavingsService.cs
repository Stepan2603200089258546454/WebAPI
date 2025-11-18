using DataContext.Abstractions.Models;

namespace Logic.Services.Interfaces
{
    public interface IHavingsService
    {
        Task AddAsync(string name, int idPosition, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IList<Havings>> GetAsync(int page, int size, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, string name, int idPosition, CancellationToken cancellationToken = default);
    }
}