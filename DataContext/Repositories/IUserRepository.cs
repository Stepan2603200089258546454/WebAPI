using DataContext.Models;

namespace DataContext.Repositories
{
    public interface IUserRepository
    {
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<bool> CreateAsync(string email, string password);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
    }
}
