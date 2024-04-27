using AutoMapper;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Domain.Entities;

namespace IncubatorApi.Application.Mapping;
public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>();
        CreateMap<RoleDto, Role>();
    }
}