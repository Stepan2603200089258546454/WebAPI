using DataContext.Abstractions.Interfaces;

namespace Logic.Services
{
    internal class HavingsService
    {
        protected readonly IHavingsRepository _repository;

        public HavingsService(IHavingsRepository repository)
        {
            _repository = repository;
        }
    }
}
