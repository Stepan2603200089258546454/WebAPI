using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Web.Endpoints
{
    internal static class TestEndpoints
    {
        public static IEndpointRouteBuilder MapTestEndpoints(this IEndpointRouteBuilder builder)
        {
#if DEBUG
            builder.MapGet("test", TestAuthAsync)
                .RequireAuthorization("JwtPolicy");
#endif
            return builder;
        }

        private static async Task<IResult> TestAuthAsync(int? id, HttpContext context)
        {
            return Results.Ok();
        }
    }
}
