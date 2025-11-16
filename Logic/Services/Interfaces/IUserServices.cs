namespace Logic.Services.Interfaces
{
    public interface IUserServices
    {
        Task<string> LoginAsync(string email, string password);
        Task<string> RegisterAsync(string email, string password);
    }
}
