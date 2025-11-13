using DataContext.Models;
using System.Security.Claims;

namespace Logic.Services
{
    internal interface IJWTProvider
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
