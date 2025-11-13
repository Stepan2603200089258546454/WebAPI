namespace Web.Endpoints.Base
{
    public static class AppEndpoints
    {
        public static IEndpointRouteBuilder UseApplicationEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapUserEndpoints();
            builder.MapTestEndpoints();

            return builder;
        }
    }
}
