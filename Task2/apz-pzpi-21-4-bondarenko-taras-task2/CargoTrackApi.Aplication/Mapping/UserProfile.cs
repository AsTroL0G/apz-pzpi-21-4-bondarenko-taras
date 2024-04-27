using AutoMapper;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Models.UpdateDto;
using CargoTrackApi.Domain.Entities;

namespace CargoTrackApi.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }


}