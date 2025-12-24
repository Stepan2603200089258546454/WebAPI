using Logic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using Web.Endpoints.Base;
using Web.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddOpenApi();

builder.UseLogic();
builder.Services.AddAutoMapper(st =>
{
    st.AddProfile<AppMappingProfile>();
});

// CORS политика "AllowAll"
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
        // .AllowCredentials() - если нужны учетные данные
        // .SetPreflightMaxAge(TimeSpan.FromMinutes(10)) - кэширование preflight
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowAll"); // Применяем политику

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

// инициализация данных и миграций
await app.Initialize();

app.Run();
