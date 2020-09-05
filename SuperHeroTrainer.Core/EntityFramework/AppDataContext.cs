using Microsoft.EntityFrameworkCore;
using SuperHeroTrainer.Core.EntityFramework;
using SuperHeroTrainer.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Core
{
    public class AppDataContext : DbContext, IDbContext
    {
        public virtual DbSet<Hero> Heroes { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
