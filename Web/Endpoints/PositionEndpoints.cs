using AutoMapper;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Request;
using Web.Models.Response;

namespace Web.Endpoints
{
    internal static class PositionEndpoints
    {
        public static IEndpointRouteBuilder MapPositionEndpoints(this IEndpointRouteBuilder builder)
        {
            RouteGroupBuilder group = builder.MapGroup("/Position")
                .RequireAuthorization("JwtPolicy");

            group.MapPost("/Get", Get);
            group.MapPost("/Update", Update);
            group.MapPost("/Delete", Delete);

            return group;
        }

        private static async Task<IResult> Get(
            GetRequest request,
            [FromServices] IPositionService service,
            [FromServices] IMapper mapper)
        {
            return TypedResults.Ok(
                mapper.Map<IList<ResponsePosition>>(
                    await service.GetAsync(request.Page, request.PageSize
                    )
                )
            );
        }
        private static async Task<IResult> Update(
            UpdatePositionRequest request,
            [FromServices] IPositionService service)
        {
            await service.UpdateAsync(request.Id, request.IdDrivingSchool, request.IdRefPosition, request.IdUser, request.Salary);
            return TypedResults.Ok();
        }
        private static async Task<IResult> Delete(
            DeleteRequest request,
            [FromServices] IPositionService service)
        {
            await service.DeleteAsync(request.Id);
            return TypedResults.Ok();
        }
    }
}
