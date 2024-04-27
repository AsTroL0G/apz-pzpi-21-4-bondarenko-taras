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
    public class NoticeProfile : Profile
    {
        public NoticeProfile()
        {
            CreateMap<NoticeDto, Notice>();
            CreateMap<Notice, NoticeDto>();
            CreateMap<NoticeCreateDto, NoticeDto>();
            CreateMap<NoticeDto, NoticeCreateDto>();
        }
    }
}
