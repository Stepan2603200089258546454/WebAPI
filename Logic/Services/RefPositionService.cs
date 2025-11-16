using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using Logic.Services.Interfaces;
using System.Threading.Tasks;

namespace Logic.Services
{
    internal class RefPositionService : IRefPositionService
    {
        protected readonly IRefPositionRepository _repository;

        public RefPositionService(IRefPositionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<RefPosition>> GetAsync(int page, int size, CancellationToken cancellationToken = default)
        {
            return await _repository.ToListAsync(page, size, cancellationToken);
        }

        public async Task AddAsync(string name, int salary, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(new RefPosition()
            {
                Name = name,
                StandardSalary = salary,
            }, cancellationToken);
        }
        public async Task UpdateAsync(int id, string name, int salary, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(new RefPosition()
            {
                Id = id,
                Name = name,
                StandardSalary = salary,
            }, cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(x => x.Id == id, cancellationToken);
        }
    }
}
