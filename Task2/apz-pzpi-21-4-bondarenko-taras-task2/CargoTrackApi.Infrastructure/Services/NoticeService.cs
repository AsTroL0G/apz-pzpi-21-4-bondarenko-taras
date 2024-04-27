using AutoMapper;
using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Infrastructure.Services
{
    public class NoticeService : INoticeService
    {
        private readonly INoticeRepository _noticeRepository;

        private readonly IMapper _mapper;

        public NoticeService(INoticeRepository noticeRepository, IMapper mapper)
        {
            _noticeRepository = noticeRepository;

            _mapper = mapper;
        }
        public async Task<NoticeDto> AddNoticeAsync(NoticeCreateDto noticeCreateDto, CancellationToken cancellationToken)
        {
            var noticeNew = new Notice
            {
                ContainerId = ObjectId.Parse(noticeCreateDto.ContainerId),
                TripId = ObjectId.Parse(noticeCreateDto.TripId),
            };

            await _noticeRepository.AddAsync(noticeNew, cancellationToken);

            return _mapper.Map<NoticeDto>(noticeNew);

        }

        public async Task<NoticeDto> UpdateNotice(NoticeDto noticeDto, CancellationToken cancellationToken)
        {
            var serv = await _noticeRepository.GetOneAsync(c => c.Id == ObjectId.Parse(noticeDto.Id), cancellationToken);

            this._mapper.Map(noticeDto, serv);

            var result = await _noticeRepository.UpdateNotice(serv, cancellationToken);

            return _mapper.Map<NoticeDto>(result);
        }
        public async Task<NoticeDto> DeleteNotice(string noticeId, CancellationToken cancellationToken)
        {
            var driverSettings = await _noticeRepository.GetOneAsync(c => c.Id == ObjectId.Parse(noticeId), cancellationToken);

            if (driverSettings == null)
            {
                throw new Exception("Notice was not found!");
            }

            await _noticeRepository.DeleteFromDbAsync(driverSettings, cancellationToken);

            return _mapper.Map<NoticeDto>(driverSettings);
        }
        public async Task<NoticeDto> GetNotice(string noticeId, CancellationToken cancellationToken)
        {
            var notice = await _noticeRepository.GetOneAsync(c => c.Id == ObjectId.Parse(noticeId), cancellationToken);
            return _mapper.Map<NoticeDto>(notice);
        }
    }
}
