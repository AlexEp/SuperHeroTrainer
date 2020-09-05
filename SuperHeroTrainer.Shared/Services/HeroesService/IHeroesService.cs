using SuperHeroTrainer.Shared.DTO;
using SuperHeroTrainer.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Shared.Services
{
    public interface IHeroesService
    {
        Task TrainHeroAsync(Hero hero);
        Task<Hero> CreateHeroAsync(string name);
        Task<ICollection<Hero>> GetHeroesAsync(UserDTO user);

        Task<Hero> GetHeroAsync(Guid id);

    }
}
