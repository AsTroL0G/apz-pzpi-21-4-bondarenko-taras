using AutoMapper;
using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.IRepositories;
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
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        private readonly IMapper _mapper;


        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }
        public async Task<ScheduleDto> AddScheduleAsync(ScheduleCreateDto scheduleCreateDto, CancellationToken cancellationToken)
        {
            var scheduleNew = new Schedule()
            {
                TripId = ObjectId.Parse(scheduleCreateDto.TripId),
                CityId = ObjectId.Parse(scheduleCreateDto.CityId),
                ArrivalTime = scheduleCreateDto.ArrivalTime,
            };

            await _scheduleRepository.AddAsync(scheduleNew, cancellationToken);

            return _mapper.Map<ScheduleDto>(scheduleCreateDto);
        }

        public async Task<ScheduleDto> UpdateSchedule(ScheduleDto scheduleDto, CancellationToken cancellationToken)
        {
            var serv = await _scheduleRepository.GetOneAsync(c => c.Id == ObjectId.Parse(scheduleDto.Id), cancellationToken);

            this._mapper.Map(scheduleDto, serv);

            var result = await _scheduleRepository.UpdateSchedule(serv, cancellationToken);

            return _mapper.Map<ScheduleDto>(result);
        }
        public async Task<ScheduleDto> DeleteSchedule(string containerId, CancellationToken cancellationToken)
        {
            var containerSettings = await _scheduleRepository.GetOneAsync(c => c.Id == ObjectId.Parse(containerId), cancellationToken);

            if (containerSettings == null)
            {
                throw new Exception("Schedule was not found!");
            }

            await _scheduleRepository.DeleteFromDbAsync(containerSettings, cancellationToken);

            return _mapper.Map<ScheduleDto>(containerSettings);
        }

        public async Task<ScheduleDto> GetSchedule(string scheduleId, CancellationToken cancellationToken)
        {
            var schedule = await _scheduleRepository.GetOneAsync(c => c.Id == ObjectId.Parse(scheduleId), cancellationToken);
            return _mapper.Map<ScheduleDto>(schedule);
        }

        public async Task<List<ScheduleDto>> GetScheduleByTripId(string tripId, CancellationToken cancellationToken)
        {
            var schedule = await _scheduleRepository.GetScheduleByTripId(tripId, cancellationToken);
            return _mapper.Map<List<ScheduleDto>>(schedule);
        }
    }
}
