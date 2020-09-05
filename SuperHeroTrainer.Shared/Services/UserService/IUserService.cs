using SuperHeroTrainer.Shared.DTO;
using SuperHeroTrainer.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Shared.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetUserAsync(string id);
        Task<UserDTO> GetUserAsync(string username, string password);
        Task<UserDTO> CreateAsync(UserDTO user, string password);
        //User RegisterUser(string username, string password);
        //DeleteUser(string username, string password);
    }
}
