using DataContext;
using Logic.Configurations;
using Logic.Services;
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
        public static void UseLogic(this IHostApplicationBuilder builder)
        {
            builder.UseDB();


            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));
            builder.Services.AddScoped<IJWTProvider, JWTProvider>();
            builder.Services.AddScoped<IUserServices, UserServices>();
        }
    }
}
