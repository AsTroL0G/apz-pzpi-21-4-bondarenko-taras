using AutoMapper;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Models.UpdateDto;
using CargoTrackApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Mapping
{

    public class ValueProfile : Profile
    {
        public ValueProfile()
        {
            CreateMap<Value, ValueDto>();
            CreateMap<ValueDto, Value>();
            CreateMap<ValueCreateDto, Value>();
            CreateMap<User, ValueCreateDto>();
            CreateMap<ValueCreateDto, ValueDto>();
            CreateMap<ValueDto, ValueCreateDto>();
        }
    }

}
