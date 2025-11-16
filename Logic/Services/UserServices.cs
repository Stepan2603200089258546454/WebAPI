using DataContext.Abstractions.Interfaces;
using DataContext.Models;
using DataContext.Repositories;
using Logic.Services.Interfaces;
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

        private async Task<string> GetJwtFromUserAsync(ApplicationUser user)
        {
            if (user is null) throw new InvalidOperationException("User is null");

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

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Jwt token</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<string> RegisterAsync(string email, string password)
        {
            ApplicationUser user = await _userRepository.CreateAsync(email, password);
            return await GetJwtFromUserAsync(user);
        }
        /// <summary>
        /// Вход пользователя в систему
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Jwt token</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<string> LoginAsync(string email, string password)
        {
            ApplicationUser user = await _userRepository.FindByEmailAsync(email);
            if (user != null && await _userRepository.CheckPasswordAsync(user, password))
            {
                return await GetJwtFromUserAsync(user);
            }
            else
            {
                throw new InvalidOperationException("Not login user");
            }
        }
    }
}
