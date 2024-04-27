using AutoMapper;
using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Paging;
using CargoTrackApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Infrastructure.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;

        private readonly IMapper _mapper;


        public SensorService(ISensorRepository sensorRepository, IMapper mapper)
        {
            _sensorRepository = sensorRepository;
            _mapper = mapper;
        }
        public async Task<PagedList<SensorDto>> GetSensorPages(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var entities = await _sensorRepository.GetPageAsync(pageNumber, pageSize, cancellationToken);
            var dtos = _mapper.Map<List<SensorDto>>(entities);
            var count = await _sensorRepository.GetTotalCountAsync(cancellationToken);
            return new PagedList<SensorDto>(dtos, pageNumber, pageSize, count);
        }
        public async Task<SensorDto> AddSensorAsync(SensorCreateDto dto, CancellationToken cancellationToken)
        {
            var sensor = new Sensor()
            {
                Name = dto.Name,
                Type = dto.Type
            };

            await _sensorRepository.AddAsync(sensor, cancellationToken);

            return _mapper.Map<SensorDto>(dto);
        }

        public async Task<SensorDto> UpdateSensor(SensorDto sensor, CancellationToken cancellationToken)
        {
            var serv = await _sensorRepository.GetOneAsync(c => c.Id == ObjectId.Parse(sensor.Id), cancellationToken);

            this._mapper.Map(sensor, serv);

            var result = await _sensorRepository.UpdateSensor(serv, cancellationToken);

            return _mapper.Map<SensorDto>(result);
        }
        public async Task<SensorDto> DeleteSensor(string sensorId, CancellationToken cancellationToken)
        {
            var sensor = await _sensorRepository.GetOneAsync(c => c.Id == ObjectId.Parse(sensorId), cancellationToken);

            if (sensor == null)
            {
                throw new Exception("Mode was not found!");
            }

            await _sensorRepository.DeleteFromDbAsync(sensor, cancellationToken);

            return _mapper.Map<SensorDto>(sensor);
        }
        public async Task<List<SensorDto>> GetSensorByType(string type, CancellationToken cancellationToken)
        {

            var result = await _sensorRepository.GetSensorByType(type, cancellationToken);

            return _mapper.Map<List<SensorDto>>(result);
        }


    }
}
