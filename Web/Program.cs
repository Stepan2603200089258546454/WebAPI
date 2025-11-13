using Logic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using Web.Endpoints.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddOpenApi();

// JWT
byte[] key = Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:Key"]!);
builder.Services.AddAuthentication(x =>
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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("JwtPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

builder.UseLogic();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//MS openAPI (/openapi/v1.json)
app.MapOpenApi();
//Scalar (/scalar/v1)
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Info API")
        .WithTheme(ScalarTheme.Kepler)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .EnableDarkMode();
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseApplicationEndpoints();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
