using DataContext.Abstractions.Interfaces;

namespace Logic.Services
{
    internal class PositionService
    {
        protected readonly IPositionRepository _repository;

        public PositionService(IPositionRepository repository)
        {
            _repository = repository;
        }
    }
}
