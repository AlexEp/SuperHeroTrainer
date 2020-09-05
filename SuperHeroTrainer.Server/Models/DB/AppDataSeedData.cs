using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperHeroTrainer.Core;
using SuperHeroTrainer.Shared.Entities;
using SuperHeroTrainer.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Models.DB
{
    public static class AppDataSeedData
    {
  
        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            AppDataContext context = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<AppDataContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            IHeroesService heroesService = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<IHeroesService>();

            if (await context.Heroes.FirstOrDefaultAsync() == null)
            {
                await heroesService.CreateHeroAsync("Superman");
                await heroesService.CreateHeroAsync("Wonder Woman");
                await heroesService.CreateHeroAsync("Flash");
                await heroesService.CreateHeroAsync("Batman");
                await heroesService.CreateHeroAsync("Iron Man");
                await heroesService.CreateHeroAsync("Wolverine");
                await heroesService.CreateHeroAsync("The Hulk");
                await heroesService.CreateHeroAsync("Professor X");
            }
      
        }
    }
}

