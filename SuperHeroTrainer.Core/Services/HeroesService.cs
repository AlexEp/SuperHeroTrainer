using Microsoft.EntityFrameworkCore;
using SuperHeroTrainer.Shared;
using SuperHeroTrainer.Shared.DTO;
using SuperHeroTrainer.Shared.Entities;
using SuperHeroTrainer.Shared.Interfaces.Repository;
using SuperHeroTrainer.Shared.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Core
{
    public class HeroesService : IHeroesService
    {
        private readonly IHeroRepository _heroRepository;

        public HeroesService(IHeroRepository heroRepository) {
            this._heroRepository = heroRepository;
        }

        public  async Task<Hero> CreateHeroAsync(string name)
        {
            var hero = new Hero()
            {
                Name = name,
                Power = Constants.INIT_HERO_POWER,
                StartingPower = Constants.INIT_HERO_POWER,
                Ability = RandomAbility(),
                SuitColor = GetRandomColor(),
            };

            await _heroRepository.InsertAsync(hero);

            return hero;
        }



        private int GetRandomColor()
        {
            Random rand = new Random();
            var r = rand.Next(0, 255);
            var g = rand.Next(0, 255);
            var b = rand.Next(0, 255);
        
            return Color.FromArgb(r,g,b).ToArgb();
        }

        public async Task TrainHeroAsync(Hero hero)
        {
            var heroFound = _heroRepository.GetById(hero.Id);

            if (heroFound == null)
            {

            }
            else {
                //Check Date of taining
                if (heroFound.LastTrainDate.HasValue)
                {
                    if (heroFound.LastTrainDate.Value.Date >= DateTime.UtcNow.Date)
                    {
                        if ( heroFound.LastTrainDateCount >= Constants.MAX_HERO_TRAININGS_PER_DAY)
                        {
                            //Do nothing !!!
                            throw new InvalidOperationException($"A hero can't train more  then {Constants.MAX_HERO_TRAININGS_PER_DAY} per day ");
                        }
                        else
                        {
                            TrainHeroPower(heroFound);
                            heroFound.LastTrainDate = DateTime.UtcNow;
                            heroFound.LastTrainDateCount++;
                            await _heroRepository.UpdateAsync(heroFound);
                        }
                    }
                    else {
                        TrainHeroPower(heroFound);
                        heroFound.LastTrainDate = DateTime.UtcNow;
                        heroFound.LastTrainDateCount = 1;
                        await  _heroRepository.UpdateAsync(heroFound);
                    }
                }
                else {

                    //first time
                    heroFound.StartedtoTrain = DateTime.UtcNow;
                    TrainHeroPower(heroFound);
                    heroFound.LastTrainDate = heroFound.StartedtoTrain;
                    heroFound.LastTrainDateCount = 1;
             
                    await _heroRepository.UpdateAsync(heroFound);
                }
            }
        }
        private void TrainHeroPower(Hero hero)
        {
            Random rand = new Random();

            var incressePowerBy = Constants.MIN_POWER_GROW + (Constants.MAX_POWER_GROW - Constants.MIN_POWER_GROW) * rand.NextDouble();
            hero.Power *= incressePowerBy;
        }
            private HeroAbility RandomAbility(){
            Random rand = new Random();
            var abilityIndexs = rand.Next(0, Enum.GetValues(typeof(HeroAbility)).Length);
            return (HeroAbility)abilityIndexs;
        }

        public async Task<ICollection<Hero>> GetHeroesAsync(UserDTO user)
        {
            return await this._heroRepository.GetAll().ToListAsync();
        }

        public async Task<Hero> GetHeroAsync(Guid id)
        {
            return await this._heroRepository.GetByIdAsync(id);
        }
    }
}
