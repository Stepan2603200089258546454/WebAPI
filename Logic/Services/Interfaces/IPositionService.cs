using DataContext.Abstractions.Models;

namespace Logic.Services.Interfaces
{
    public interface IPositionService
    {
        Task AddAsync(int idDrivingSchool, int idRefPosition, string idUser, int salary, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<IList<Position>> GetAsync(int page, int size, CancellationToken cancellationToken = default);
        Task UpdateAsync(int id, int idDrivingSchool, int idRefPosition, string idUser, int salary, CancellationToken cancellationToken = default);
    }
}