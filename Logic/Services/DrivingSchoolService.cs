using DataContext.Abstractions.Interfaces;

namespace Logic.Services
{
    internal class DrivingSchoolService
    {
        protected readonly IDrivingSchoolRepository _repository;

        public DrivingSchoolService(IDrivingSchoolRepository repository)
        {
            _repository = repository;
        }
    }
}
