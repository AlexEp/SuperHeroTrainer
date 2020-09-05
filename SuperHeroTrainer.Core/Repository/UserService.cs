
using Microsoft.AspNetCore.Identity;
using SuperHeroTrainer.Core.EntityFramework;
using SuperHeroTrainer.Core.Identity;
using SuperHeroTrainer.Shared.DTO;
using SuperHeroTrainer.Shared.Entities;
using SuperHeroTrainer.Shared.Interfaces.Repository;
using SuperHeroTrainer.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Core.Repository
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;

        public UserService(
            UserManager<AppIdentityUser> userManager, 
            SignInManager<AppIdentityUser> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserDTO> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return null;

            return new UserDTO() {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Email = user.Email,
            };
        }

        public async Task<UserDTO> CreateAsync(UserDTO user, string password) {
            var foundUser = await _userManager.FindByNameAsync(user.UserName);

            if (foundUser != null)
                throw new InvalidOperationException("Username already exists");

            AppIdentityUser userToCreate = new AppIdentityUser(user.UserName)
            {
                FirstName = user.FirstName,
                Email = user.Email,
            };
            IdentityResult result = await _userManager.CreateAsync(userToCreate, password);

            if (!result.Succeeded) {
                throw new AggregateException(result.Errors.Select(e => new InvalidOperationException(e.Description)));
            }

            user.Id = userToCreate.Id;
            return user; 
        }
 


        public async Task<UserDTO> GetUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return new UserDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    Email = user.Email,
                };
            }

            return null;
          
        }
    }
}
