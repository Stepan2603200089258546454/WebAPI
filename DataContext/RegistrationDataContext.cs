using DataContext.Abstractions.Interfaces;
using DataContext.Context;
using DataContext.Models;
using DataContext.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public static class RegistrationDataContext
    {
        public static async Task AutoMigrate(this IHost host)
        {
            // Миграции EF Core
            using (IServiceScope scope = host.Services.CreateScope())
            {
                using (ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    if ((await dbContext.Database.GetPendingMigrationsAsync())?.Any() == true) //проверяем нужны ли миграции
                        dbContext.Database.Migrate(); //Пытаемся актуализировать и принять миграции
                }
            }
        }
        public static void UseDB(this IHostApplicationBuilder builder)
        {
            string connectionString = builder.Configuration["PostgreSettings:DefaultConnection"] ?? throw new InvalidOperationException("Строка подключения «DefaultConnection» не найдена.");
            string version = builder.Configuration["PostgreSettings:Version"] ?? throw new InvalidOperationException("Не указана версия PostgreSQL");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("MemoryDB");

                //options.UseNpgsql(connectionString, npgsqlOptions =>
                //{
                //    npgsqlOptions.SetPostgresVersion(Version.Parse(version));
                //});
            });

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IDrivingSchoolRepository, DrivingSchoolRepository>();
            builder.Services.AddScoped<IHavingsRepository, HavingsRepository>();
            builder.Services.AddScoped<IPositionRepository, PositionRepository>();
            builder.Services.AddScoped<IRefPositionRepository, RefPositionRepository>();
        }
    }
}
