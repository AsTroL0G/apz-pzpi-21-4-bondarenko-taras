using AutoMapper;
using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Paging;
using CargoTrackApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Infrastructure.Services
{
    public class SensorsServices : ISensorsService
    {
        private readonly ISensorsRepository _sensorsRepository;

        private readonly ISensorRepository _sensorRepository;

        private readonly IMapper _mapper;


        public SensorsServices(ISensorsRepository sensorsRepository, ISensorRepository sensorRepository, IMapper mapper)
        {
            _sensorsRepository = sensorsRepository;
            _sensorRepository = sensorRepository;
            _mapper = mapper;
        }

        public async Task<SensorsDto> AddSensorsAsync(SensorsCreateDto dto, CancellationToken cancellationToken)
        {
            var sensors = new Sensors()
            {
                SensorId = ObjectId.Parse(dto.SensorId),
                ContainerId = ObjectId.Parse(dto.ContainerId)
            };

            await _sensorsRepository.AddAsync(sensors, cancellationToken);

            return _mapper.Map<SensorsDto>(dto);
        }

        public async Task<SensorsDto> UpdateSensors(SensorsDto sensors, CancellationToken cancellationToken)
        {
            var serv = await _sensorsRepository.GetOneAsync(c => c.Id == ObjectId.Parse(sensors.Id), cancellationToken);

            this._mapper.Map(sensors, serv);

            var result = await _sensorsRepository.UpdateSensors(serv, cancellationToken);

            return _mapper.Map<SensorsDto>(result);
        }
        public async Task<SensorsDto> DeleteSensors(string sensorsId, CancellationToken cancellationToken)
        {
            var sensors = await _sensorsRepository.GetOneAsync(c => c.Id == ObjectId.Parse(sensorsId), cancellationToken);

            if (sensors == null)
            {
                throw new Exception("Mode was not found!");
            }

            await _sensorsRepository.DeleteFromDbAsync(sensors, cancellationToken);

            return _mapper.Map<SensorsDto>(sensors);
        }

        public async Task<List<SensorsDto>> GetSensorsById(string containerId, CancellationToken cancellationToken)
        {
            var value = await _sensorsRepository.GetSensorsById(containerId, cancellationToken);
            return _mapper.Map<List<SensorsDto>>(value);
        }
        public async Task<SensorsDto> GetSensorsByType(string type, string containerId, CancellationToken cancellationToken)
        {

            var sensor = await _sensorRepository.GetSensorByType(type, cancellationToken);
            var sensors = await _sensorsRepository.FindSensorsByGPS(sensor, cancellationToken);
            var value = await _sensorsRepository.GetSensorsByType(sensors, containerId, cancellationToken);
            return _mapper.Map<SensorsDto>(value);
        }
        public async Task<PagedList<SensorsDto>> GetAllSensors(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var entities = await _sensorsRepository.GetPageAsync(pageNumber, pageSize, cancellationToken);
            var dtos = _mapper.Map<List<SensorsDto>>(entities);
            var count = await _sensorsRepository.GetTotalCountAsync(cancellationToken);
            return new PagedList<SensorsDto>(dtos, pageNumber, pageSize, count);
        }




    }
}
