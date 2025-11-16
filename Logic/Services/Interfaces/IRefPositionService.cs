using DataContext.Abstractions.Models;

namespace Logic.Services.Interfaces
{
    public interface IRefPositionService
    {
        Task AddAsync(string name, int salary, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IList<RefPosition>> GetAsync(int page, int size, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, string name, int salary, CancellationToken cancellationToken = default);
    }
}
