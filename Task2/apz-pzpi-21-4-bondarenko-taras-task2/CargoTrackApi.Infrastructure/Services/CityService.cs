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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        private readonly IMapper _mapper;


        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<CityDto> AddCityAsync(CityCreateDto cityCreateDto, CancellationToken cancellationToken)
        {
            var cityNew = new City()
            {
                Name = cityCreateDto.Name,
                District = cityCreateDto.District,
                Сountry = cityCreateDto.Сountry,
            };

            await _cityRepository.AddAsync(cityNew, cancellationToken);

            return _mapper.Map<CityDto>(cityCreateDto);
        }

        public async Task<CityDto> UpdateCity(CityDto cityDto, CancellationToken cancellationToken)
        {
            var serv = await _cityRepository.GetOneAsync(c => c.Id == ObjectId.Parse(cityDto.Id), cancellationToken);

            this._mapper.Map(cityDto, serv);

            var result = await _cityRepository.UpdateCity(serv, cancellationToken);

            return _mapper.Map<CityDto>(result);
        }
        public async Task<CityDto> DeleteCity(string cityId, CancellationToken cancellationToken)
        {
            var citySettings = await _cityRepository.GetOneAsync(c => c.Id == ObjectId.Parse(cityId), cancellationToken);

            if (citySettings == null)
            {
                throw new Exception("City was not found!");
            }

            await _cityRepository.DeleteFromDbAsync(citySettings, cancellationToken);

            return _mapper.Map<CityDto>(citySettings);
        }
        public async Task<CityDto> GetCity(string cityId, CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetOneAsync(c => c.Id == ObjectId.Parse(cityId), cancellationToken);
            return _mapper.Map<CityDto>(city);
        }

    }
}
