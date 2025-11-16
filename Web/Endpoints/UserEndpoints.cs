using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        /// <summary>
        /// В куки устанавливает токен, только для отладки
        /// </summary>
        [Conditional("DEBUG")]
        private static void AppendCookiesDebug(HttpContext context, string token)
        {
            context.Response.Cookies.Append(JwtBearerDefaults.AuthenticationScheme, token);
        }

        private static async Task<IResult> RegisterAsync(
            RegisterUserRequest request,
            HttpContext context,
            [FromServices] IUserServices userService)
        {
            try
            {
                string token = await userService.RegisterAsync(request.Email, request.Password);

                AppendCookiesDebug(context, token);

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
        private static async Task<IResult> LoginAsync(
            LoginUserRequest request,
            HttpContext context,
            [FromServices] IUserServices userService)
        {
            try
            {
                string token = await userService.LoginAsync(request.Email, request.Password);

                AppendCookiesDebug(context, token);

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
