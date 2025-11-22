using DataContext.Abstractions.Interfaces;
using DataContext.Abstractions.Models;
using DataContext.Context;
using DataContext.Repositories;
using Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public static void ConfigureDB(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataBaseOptions>(configuration.GetSection(nameof(DataBaseOptions)));
        }
        public static void ConfigureDBContext(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>((provider, options) =>
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
        }
        public static void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
        }
        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDrivingSchoolRepository, DrivingSchoolRepository>();
            services.AddScoped<IHavingsRepository, HavingsRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IRefPositionRepository, RefPositionRepository>();
        }
        public static void UseDB(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDB(services, configuration);

            ConfigureDBContext(services);

            services.AddDatabaseDeveloperPageExceptionFilter();

            ConfigureIdentity(services);

            ConfigureRepositories(services);
        }
        public static void UseDB(this IHostApplicationBuilder builder)
        {
            UseDB(builder.Services, builder.Configuration);
        }
    }
}
