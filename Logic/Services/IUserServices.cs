
namespace Logic.Services
{
    public interface IUserServices
    {
        Task<string> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password);
    }
}
