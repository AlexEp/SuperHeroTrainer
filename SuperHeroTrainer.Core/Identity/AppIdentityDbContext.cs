using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperHeroTrainer.Core.EntityFramework;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Core.Identity
{
    public class AppIdentityUser : IdentityUser
    {
        public AppIdentityUser(string userName) : base(userName)
        {
       
        }
        public string FirstName { get; set; }
    }

    public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser>, IDbContext
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options) { }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
