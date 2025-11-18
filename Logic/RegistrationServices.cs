using DataContext;
using Domain.Options;
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
            using (IServiceScope scope = host.Services.CreateScope())
            {
                await RegistrationDataContext.AutoMigrate(scope);

                IRefPositionService refPositionService = scope.ServiceProvider.GetRequiredService<IRefPositionService>();
                await refPositionService.AddAsync("Директор", 75000);
                await refPositionService.AddAsync("Преподаватель", 60000);
                await refPositionService.AddAsync("Инструктор", 60000);
                await refPositionService.AddAsync("Уборщик", 45000);
                IDrivingSchoolService drivingSchoolService = scope.ServiceProvider.GetRequiredService<IDrivingSchoolService>();
                await drivingSchoolService.AddAsync("Драйв", "Москва");
                await drivingSchoolService.AddAsync("Перекресток", "Москва");
                await drivingSchoolService.AddAsync("Колесо", "Москва");
                await drivingSchoolService.AddAsync("Драйв", "Домодедово");
                await drivingSchoolService.AddAsync("Колесо", "Калининец");
            }
        }
        public static void UseLogic(this IHostApplicationBuilder builder)
        {
            builder.UseDB();

            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));
            builder.Services.AddScoped<IJWTProvider, JWTProvider>();
            builder.Services.AddScoped<IUserServices, UserServices>();

            builder.Services.AddScoped<IRefPositionService, RefPositionService>();
            builder.Services.AddScoped<IPositionService, PositionService>();
            builder.Services.AddScoped<IHavingsService, HavingsService>();
            builder.Services.AddScoped<IDrivingSchoolService, DrivingSchoolService>();
        }
    }
}
