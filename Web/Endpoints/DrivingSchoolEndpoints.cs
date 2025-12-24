using AutoMapper;
using Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Request;
using Web.Models.Response;

namespace Web.Endpoints
{
    internal static class DrivingSchoolEndpoints
    {
        public static IEndpointRouteBuilder MapDrivingSchoolEndpoints(this IEndpointRouteBuilder builder)
        {
            RouteGroupBuilder group = builder.MapGroup("/DrivingSchool")
                .RequireAuthorization("JwtPolicy");

            group.MapPost("/Get", Get);
            group.MapPost("/Update", Update);
            group.MapPost("/Add", Add);
            group.MapPost("/Delete", Delete);

            return group;
        }

        private static async Task<IResult> Get(
            GetRequest request,
            [FromServices] IDrivingSchoolService service,
            [FromServices] IMapper mapper)
        {
            return TypedResults.Ok(
                mapper.Map<IList<ResponseDrivingSchool>>(
                    await service.GetAsync(request.Page, request.PageSize
                    )
                )
            );
        }
        private static async Task<IResult> Add(
            AddDrivingSchoolRequest request,
            [FromServices] IDrivingSchoolService service)
        {
            await service.AddAsync(request.Name, request.Adress);
            return TypedResults.Ok();
        }
        private static async Task<IResult> Update(
            UpdateDrivingSchoolRequest request,
            [FromServices] IDrivingSchoolService service)
        {
            await service.UpdateAsync(request.Id, request.Name, request.Adress);
            return TypedResults.Ok();
        }
        private static async Task<IResult> Delete(
            DeleteRequest request,
            [FromServices] IDrivingSchoolService service)
        {
            await service.DeleteAsync(request.Id);
            return TypedResults.Ok();
        }
    }
}
