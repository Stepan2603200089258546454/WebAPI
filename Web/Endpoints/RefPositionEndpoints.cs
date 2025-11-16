using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Request;

namespace Web.Endpoints
{
    internal static class RefPositionEndpoints
    {
        public static IEndpointRouteBuilder MapRefPositionEndpoints(this IEndpointRouteBuilder builder)
        {
            RouteGroupBuilder group = builder.MapGroup("/RefPosition")
                .RequireAuthorization("JwtPolicy");

            group.MapPost("/Get", Get);
            group.MapPost("/Update", Update);
            group.MapPost("/Delete", Delete);

            return group;
        }

        private static async Task<IResult> Get(
            GetRequest request,
            [FromServices] IRefPositionService service)
        {
            return TypedResults.Ok(await service.GetAsync(request.Page, request.PageSize));
        }
        private static async Task<IResult> Update(
            UpdateRefPositionRequest request,
            [FromServices] IRefPositionService service)
        {
            await service.UpdateAsync(request.Id, request.Name, request.Salary);
            return TypedResults.Ok();
        }
        private static async Task<IResult> Delete(
            DeleteRefPositionRequest request,
            [FromServices] IRefPositionService service)
        {
            await service.DeleteAsync(request.Id);
            return TypedResults.Ok();
        }
    }
}
