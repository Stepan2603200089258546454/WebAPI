using DataContext.Models;
using DataContext.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    internal class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTProvider _jwtProvider;
        public UserServices(IUserRepository userRepository, IJWTProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            return await _userRepository.CreateAsync(email, password);
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            ApplicationUser user = await _userRepository.FindByEmailAsync(email);
            if (user != null && await _userRepository.CheckPasswordAsync(user, password))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Authentication, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName?.ToString() ?? string.Empty),
                    new Claim(ClaimTypes.Email, user.Email?.ToString() ?? string.Empty),
                };

                foreach (string role in await _userRepository.GetRolesAsync(user))
                    claims.Add(new Claim(ClaimTypes.Role, role));

                return _jwtProvider.GenerateToken(claims);
            }
            else
            {
                throw new InvalidOperationException("Not login user");
            }
        }
    }
}
