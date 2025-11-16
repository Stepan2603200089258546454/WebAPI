using DataContext;
using Logic.Configurations;
using Logic.Services;
using Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class RegistrationServices
    {
        public static async Task Initialize(this IHost host)
        {
            // так как бд в памяти не требуется
            //await host.AutoMigrate();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IRefPositionService refPositionService = scope.ServiceProvider.GetRequiredService<IRefPositionService>();
                await refPositionService.AddAsync("Директор", 75000);
                await refPositionService.AddAsync("Преподаватель", 60000);
                await refPositionService.AddAsync("Инструктор", 60000);
                await refPositionService.AddAsync("Уборщик", 45000);
            }
        }
        public static void UseLogic(this IHostApplicationBuilder builder)
        {
            builder.UseDB();


            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));
            builder.Services.AddScoped<IJWTProvider, JWTProvider>();
            builder.Services.AddScoped<IUserServices, UserServices>();

            builder.Services.AddScoped<IRefPositionService, RefPositionService>();
        }
    }
}
