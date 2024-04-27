using AutoMapper;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Mapping
{
    public class SensorsProfile : Profile
    {
        public SensorsProfile()
        {
            CreateMap<Sensors, SensorsDto>();
            CreateMap<SensorsDto, Sensors>();
            CreateMap<SensorsCreateDto, Sensors>();
            CreateMap<Sensors, SensorsCreateDto>();
            CreateMap<SensorsCreateDto, SensorsDto>();
            CreateMap<SensorsDto, SensorsCreateDto>();
        }
    }
}
