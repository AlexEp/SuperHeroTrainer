using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SuperHeroTrainer.Models;
using AutoMapper;
using SuperHeroTrainer.Core;
using SuperHeroTrainer.Shared.Interfaces.Repository;
using SuperHeroTrainer.Core.Repository;
using SuperHeroTrainer.Shared.Services;
using SuperHeroTrainer.Core.Identity;
using SuperHeroTrainer.Models.DB;
using Autofac;
using SuperHeroTrainer.Core.EntityFramework;

namespace SuperHeroTrainer
{
    public class Startup
    {
        private string CORS_POLICY = "cors_policy";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new IocConfigModule());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            /* *** [App settings] *** */
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            AppSettings appSettings = appSettingsSection.Get<AppSettings>();

            /* *** [DAL] *** */

            services.AddDbContext<AppDataContext>(options => options.UseSqlServer(appSettings.DB.DefaultConnection));
           
            
            /* *** [Authentication] *** */

            //DB Identity Context

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(appSettings.DB.IdentityConnection));
             
            services.AddIdentity<AppIdentityUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppIdentityDbContext>();


            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Home/Login";
            //});
            //Jwt Tokens
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false; //No HTTPS for now
                       options.SaveToken = true;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = false,
                           ValidateAudience = false,
                           //ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           //ValidIssuer = appSettings.JWT.Issuer,
                           //ValidAudience = appSettings.JWT.Audience,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWT.SecretKey)),
                           ClockSkew = TimeSpan.Zero
                       };
                   });

            //cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: CORS_POLICY,
                                  builder =>
                                  {
                                      builder.WithOrigins(appSettings.CORS.FrontEndURL)
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials();
                                  });
            });

            /* *** [Else] *** */
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(CORS_POLICY);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });


            //Seed data
            IdentitySeedData.EnsurePopulatedAsync(app).Wait();
            AppDataSeedData.EnsurePopulatedAsync(app).Wait();
        }
    }


    public class IocConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(context => new AppDataContext(context.Resolve<DbContextOptions<AppDataContext>>()))
 .                          As<IDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<HeroRepository>().As<IHeroRepository>().InstancePerLifetimeScope();
            builder.RegisterType<HeroesService>().As<IHeroesService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}
