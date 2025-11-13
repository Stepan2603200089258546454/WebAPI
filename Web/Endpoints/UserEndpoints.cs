using Logic.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Request;

namespace Web.Endpoints
{
    internal static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("register", RegisterAsync);
            builder.MapPost("login", LoginAsync);

            return builder;
        }

        private static async Task<IResult> RegisterAsync(
            RegisterUserRequest request,
            HttpContext context,
            [FromServices] IUserServices userService)
        {
            bool result = await userService.RegisterAsync(request.Email, request.Password);
            if (result)
            {
                string token = await userService.LoginAsync(request.Email, request.Password);

                context.Response.Cookies.Append(JwtBearerDefaults.AuthenticationScheme, token);

                return Results.Ok(token);
            }
            else
                return Results.BadRequest("Error from register");
        }
        private static async Task<IResult> LoginAsync(
            LoginUserRequest request,
            HttpContext context,
            [FromServices] IUserServices userService)
        {
            try
            {
                string token = await userService.LoginAsync(request.Email, request.Password);

                context.Response.Cookies.Append(JwtBearerDefaults.AuthenticationScheme, token);

                return Results.Ok(token);
            }
            catch (Exception ex)
            {
#if DEBUG
                return Results.BadRequest(ex.Message);
#else
                return Results.BadRequest("Error");
#endif
            }
        }
    }
}
