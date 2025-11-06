namespace WebAPI.Endpoints
{
    internal static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("register", RegisterAsync);
            builder.MapPost("login", LoginAsync);

            return builder;
        }

        private static async Task<IResult> RegisterAsync()
        {
            return Results.Ok();
        }
        private static async Task<IResult> LoginAsync()
        {
            return Results.Ok();
        }
    }
}
