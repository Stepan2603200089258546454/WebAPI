using DataContext;
using Domain.Options;
using Logic.Services;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        public static void ConfigureOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTOptions>(configuration.GetSection(nameof(JWTOptions)));
        }
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IJWTProvider, JWTProvider>();
            services.AddScoped<IUserServices, UserServices>();

            services.AddScoped<IRefPositionService, RefPositionService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IHavingsService, HavingsService>();
            services.AddScoped<IDrivingSchoolService, DrivingSchoolService>();
        }
        public static void ConfigureJwt(IServiceCollection services, IConfiguration configuration)
        {
            // JWT
            byte[] key = Encoding.UTF8.GetBytes(configuration["JWTOptions:Key"]!);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,

                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                    // установка ключа безопасности
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    // указывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = false,
                    // строка, представляющая издателя
                    //ValidIssuer = AuthOptions.ISSUER,

                    // будет ли валидироваться потребитель токена
                    ValidateAudience = false,
                    // установка потребителя токена
                    //ValidAudience = AuthOptions.AUDIENCE,
                };
#if DEBUG
                // Чтобы не вписывать каждый раз заголовок авторизации
                x.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        // сначала пробуем взять заголовок (примерно продуктовая версия)
                        // Authorization: Bearer {JWT Token}
                        if (context.Token is null)
                        {
                            string[]? headerItems = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ");
                            if (headerItems?.Length == 2)
                            {
                                if (string.Equals(headerItems.First(), JwtBearerDefaults.AuthenticationScheme, StringComparison.InvariantCultureIgnoreCase))
                                    context.Token = headerItems.Last();
                            }
                        }
                        // заголовка нет пытаемся вытащить куки
                        if (context.Token is null)
                            context.Token = context.Request.Cookies[JwtBearerDefaults.AuthenticationScheme];

                        return Task.CompletedTask;
                    }
                };
#endif
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("JwtPolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });
        }
        public static void UseLogic(IServiceCollection services, IConfiguration configuration)
        {
            RegistrationDataContext.UseDB(services, configuration);

            ConfigureOptions(services, configuration);

            ConfigureJwt(services, configuration);

            ConfigureServices(services);
        }
        public static void UseLogic(this IHostApplicationBuilder builder)
        {
            UseLogic(builder.Services, builder.Configuration);
        }
    }
}
