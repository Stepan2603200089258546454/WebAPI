using DataContext.Abstractions.Models;

namespace Logic.Services.Interfaces
{
    public interface IDrivingSchoolService
    {
        Task AddAsync(string name, string adress, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IList<DrivingSchool>> GetAsync(int page, int size, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, string name, string adress, CancellationToken cancellationToken = default);
    }
}