using DataContext.Abstractions.Interfaces;
using DataContext.Context;
using DataContext.Models;
using DataContext.Repositories;
using Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
                await AutoMigrate(scope);
            }
        }
        public static async Task AutoMigrate(IServiceScope scope)
        {
            DataBaseOptions dbOptions = scope.ServiceProvider.GetRequiredService<IOptions<DataBaseOptions>>().Value;
            // Актуализируем БД только когда она не в памяти
            if (dbOptions.DBType != DBType.InMemory)
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
            builder.Services.Configure<DataBaseOptions>(builder.Configuration.GetSection(nameof(DataBaseOptions)));

            builder.Services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                DataBaseOptions dbOptions = provider.GetRequiredService<IOptions<DataBaseOptions>>().Value;
                
                if (dbOptions.DBType == DBType.InMemory)
                {
                    if (dbOptions.MemorySettings is null) throw new ArgumentNullException(nameof(dbOptions.MemorySettings), "Настройки подключения к InMemory не найдены");
                    options.UseInMemoryDatabase(dbOptions.MemorySettings.Name);
                }
                else if (dbOptions.DBType == DBType.PostgreSQL)
                {
                    if (dbOptions.PostgreSettings is null) throw new ArgumentNullException(nameof(dbOptions.PostgreSettings), "Настройки подключения к PostgreSQL не найдены");
                    options.UseNpgsql(dbOptions.PostgreSettings.DefaultConnection, npgsqlOptions =>
                    {
                        npgsqlOptions.SetPostgresVersion(Version.Parse(dbOptions.PostgreSettings.Version));
                    });
                }
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
