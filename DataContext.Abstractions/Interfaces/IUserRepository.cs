using DataContext.Abstractions.Models;

namespace DataContext.Abstractions.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<ApplicationUser> CreateAsync(string email, string password);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
    }
}
