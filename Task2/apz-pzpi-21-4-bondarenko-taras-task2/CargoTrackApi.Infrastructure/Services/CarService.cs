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
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        private readonly IMapper _mapper;


        public CarService(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CarDto> GetCar(string carId, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetOneAsync(c => c.Id == ObjectId.Parse(carId), cancellationToken);
            return _mapper.Map<CarDto>(car);
        }

        public async Task<CarDto> AddCarAsync(CarCreateDto carCreateDto, CancellationToken cancellationToken)
        {
            var carNew = new Car()
            {

                Number_MIA = carCreateDto.Number_MIA,

                Brand = carCreateDto.Brand,
                
                LoadCapacity = carCreateDto.LoadCapacity,
            };

            await _carRepository.AddAsync(carNew, cancellationToken);

            return _mapper.Map<CarDto>(carCreateDto);
        }

        public async Task<CarDto> UpdateCar(CarDto carDto, CancellationToken cancellationToken)
        {
            var serv = await _carRepository.GetOneAsync(c => c.Id == ObjectId.Parse(carDto.Id), cancellationToken);

            this._mapper.Map(carDto, serv);

            var result = await _carRepository.UpdateCar(serv, cancellationToken);

            return _mapper.Map<CarDto>(result);
        }
        public async Task<CarDto> DeleteCar(string carId, CancellationToken cancellationToken)
        {
            var carSettings = await _carRepository.GetOneAsync(c => c.Id == ObjectId.Parse(carId), cancellationToken);

            if (carSettings == null)
            {
                throw new Exception("Car was not found!");
            }

            await _carRepository.DeleteFromDbAsync(carSettings, cancellationToken);

            return _mapper.Map<CarDto>(carSettings);
        }

    }
}
