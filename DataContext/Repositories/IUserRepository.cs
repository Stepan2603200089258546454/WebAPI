using DataContext.Models;

namespace DataContext.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(string email, string password);
        Task<UserEntity> Get(string email);
    }
}
