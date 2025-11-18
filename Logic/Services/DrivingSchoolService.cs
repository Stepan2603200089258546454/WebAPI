using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    internal class DrivingSchoolService : IDrivingSchoolService
    {
        protected readonly IDrivingSchoolRepository _repository;

        public DrivingSchoolService(IDrivingSchoolRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<DrivingSchool>> GetAsync(int page, int size, CancellationToken cancellationToken = default)
        {
            return await _repository.ToListAsync(page, size, cancellationToken);
        }

        public async Task AddAsync(string name, string adress, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(new DrivingSchool()
            {
                Name = name,
                Adress = adress,
            }, cancellationToken);
        }
        public async Task UpdateAsync(int id, string name, string adress, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(new DrivingSchool()
            {
                Id = id,
                Name = name,
                Adress = adress,
            }, cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(x => x.Id == id, cancellationToken);
        }
    }
}
