using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    internal class PositionService : IPositionService
    {
        protected readonly IPositionRepository _repository;

        public PositionService(IPositionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<Position>> GetAsync(int page, int size, CancellationToken cancellationToken = default)
        {
            return await _repository.ToListAsync(page, size, cancellationToken);
        }

        public async Task AddAsync(int idDrivingSchool, int idRefPosition, string idUser, int salary, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(new Position()
            {
                IdDrivingSchool = idDrivingSchool,
                IdRefPosition = idRefPosition,
                IdUser = idUser,
                Salary = salary,
            }, cancellationToken);
        }
        public async Task UpdateAsync(int id, int idDrivingSchool, int idRefPosition, string idUser, int salary, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(new Position()
            {
                Id = id,
                IdDrivingSchool = idDrivingSchool,
                IdRefPosition = idRefPosition,
                IdUser = idUser,
                Salary = salary,
            }, cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(x => x.Id == id, cancellationToken);
        }
    }
}
