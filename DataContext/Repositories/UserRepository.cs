using DataContext.Abstractions.Interfaces;
using DataContext.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> CreateAsync(string email, string password)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };
            IdentityResult result = await _userManager.CreateAsync(user, password);
            return result.Succeeded ? user : throw new InvalidOperationException(string.Join("; ", result.Errors.Select(x => x.Description)));
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) ?? throw new InvalidOperationException("User not found");
        }
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
