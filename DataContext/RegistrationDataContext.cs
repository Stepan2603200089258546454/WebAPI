using DataContext.Context;
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
        /*
         https://rutube.ru/video/55efc587951eebca21877f93a6040bf9/
         */
        public static async Task AutoMigrate(this IHost host)
        {
            // Миграции EF Core
            using (IServiceScope scope = host.Services.CreateScope())
            {
                using (DBContext dbContext = scope.ServiceProvider.GetRequiredService<DBContext>())
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

            builder.Services.AddDbContextFactory<DBContext>(options =>
            {
                options.UseInMemoryDatabase("MemoryDB");

                //options.UseNpgsql(connectionString, npgsqlOptions =>
                //{
                //    npgsqlOptions.SetPostgresVersion(Version.Parse(version));
                //});
            });
        }
    }
}
