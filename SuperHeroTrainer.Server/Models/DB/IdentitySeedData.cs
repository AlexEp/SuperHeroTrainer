using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperHeroTrainer.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Models.DB
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Admin2020!";
        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            AppIdentityDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<AppIdentityDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            UserManager<AppIdentityUser> userManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<UserManager<AppIdentityUser>>();
            var user = await userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new AppIdentityUser(adminUser);
                user.UserName = adminUser;
                user.Email = "admin@example.com";
                var results = await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}

