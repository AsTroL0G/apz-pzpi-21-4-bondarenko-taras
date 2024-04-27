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
    public class SensorProfile : Profile
    {
        public SensorProfile()
        {
            CreateMap<Sensor, SensorDto>();
            CreateMap<SensorDto, Sensor>();
            CreateMap<SensorUpdateDto, Sensor>();
            CreateMap<Sensor, SensorUpdateDto>();
            CreateMap<SensorUpdateDto, SensorDto>();
            CreateMap<SensorDto, SensorUpdateDto>();
            CreateMap<SensorCreateDto, SensorDto>();
            CreateMap<SensorDto, SensorCreateDto>();
        }
    }


}
