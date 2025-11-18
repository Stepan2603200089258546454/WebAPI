using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    internal class HavingsService : IHavingsService
    {
        protected readonly IHavingsRepository _repository;

        public HavingsService(IHavingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<Havings>> GetAsync(int page, int size, CancellationToken cancellationToken = default)
        {
            return await _repository.ToListAsync(page, size, cancellationToken);
        }

        public async Task AddAsync(string name, int idPosition, CancellationToken cancellationToken = default)
        {
            await _repository.AddAsync(new Havings()
            {
                Name = name,
                IdPosition = idPosition,
            }, cancellationToken);
        }
        public async Task UpdateAsync(int id, string name, int idPosition, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(new Havings()
            {
                Id = id,
                Name = name,
                IdPosition = idPosition,
            }, cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(x => x.Id == id, cancellationToken);
        }
    }
}
