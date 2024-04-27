using AutoMapper;
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

    public class ValueServices : IValueService
    {
        private readonly IValueRepository _valueRepository;

        private readonly IMapper _mapper;


        public ValueServices(IValueRepository valueRepository, IMapper mapper)
        {
            _valueRepository = valueRepository;
            _mapper = mapper;
        }

        public async Task<ValueDto> AddValueAsync(ValueCreateDto dto, CancellationToken cancellationToken)
        {
            var value = new Value()
            {

                SensorId = ObjectId.Parse(dto.SensorId),

                Values = dto.Values,

                MeasurementTime = dto.MeasurementTime,
            };

            await _valueRepository.AddAsync(value, cancellationToken);

            return _mapper.Map<ValueDto>(dto);
        }

        public async Task<ValueDto> UpdateValue(ValueDto value, CancellationToken cancellationToken)
        {
            var serv = await _valueRepository.GetOneAsync(c => c.Id == ObjectId.Parse(value.Id), cancellationToken);

            this._mapper.Map(value, serv);

            var result = await _valueRepository.UpdateValue(serv, cancellationToken);

            return _mapper.Map<ValueDto>(result);
        }
        public async Task<ValueDto> DeleteValue(string modeSettingsId, CancellationToken cancellationToken)
        {
            var modeSettings = await _valueRepository.GetOneAsync(c => c.Id == ObjectId.Parse(modeSettingsId), cancellationToken);

            if (modeSettings == null)
            {
                throw new Exception("Mode was not found!");
            }

            await _valueRepository.DeleteFromDbAsync(modeSettings, cancellationToken);

            return _mapper.Map<ValueDto>(modeSettings);
        }

        public async Task<List<ValueDto>> GetValueBySensorId(string sensorId, CancellationToken cancellationToken)
        {
            var value = await _valueRepository.GetValueBySensorId(sensorId, cancellationToken);
            return _mapper.Map<List<ValueDto>>(value);
        }

    }
}
