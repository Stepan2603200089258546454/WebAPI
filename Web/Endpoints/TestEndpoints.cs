using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Web.Endpoints
{
    internal static class TestEndpoints
    {
        public static IEndpointRouteBuilder MapTestEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("test", TestAuthAsync)
                .RequireAuthorization("JwtPolicy");

            return builder;
        }

        private static async Task<IResult> TestAuthAsync(int? id, HttpContext context)
        {

            return Results.Ok();
        }
    }
}
