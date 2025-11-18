using AutoMapper;
using DataContext.Abstractions.Models;
using System;
using Web.Models.Response;

namespace Web.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<DrivingSchool, ResponseDrivingSchool>().ReverseMap();
            CreateMap<Havings, ResponseHavings>().ReverseMap();
            CreateMap<RefPosition, ResponseRefPosition>().ReverseMap();
            CreateMap<Position, ResponsePosition>().ReverseMap();
        }
    }
}
