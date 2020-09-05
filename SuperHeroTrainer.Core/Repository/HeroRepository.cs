
using SuperHeroTrainer.Core.EntityFramework;
using SuperHeroTrainer.Shared.Entities;
using SuperHeroTrainer.Shared.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHeroTrainer.Core.Repository
{
    public class HeroRepository : Repository<Hero>, IHeroRepository
    {
        public HeroRepository(IDbContext context) : base(context)
        {

        }
    }
}
