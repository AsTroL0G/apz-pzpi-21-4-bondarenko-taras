using AutoMapper;
using CargoTrackApi.Aplication.IServices;
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

    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        private readonly IMapper _mapper;


        public TripService(ITripRepository tripRepository, IMapper mapper)
        {
            _tripRepository = tripRepository;
            _mapper = mapper;
        }
        public async Task<TripDto> AddTripAsync(TripCreateDto tripCreateDto, CancellationToken cancellationToken)
        {
            var tripNew = new Trip()
            {
                CarId = ObjectId.Parse(tripCreateDto.CarId),
                DriverId = ObjectId.Parse(tripCreateDto.DriverId),
            };

            await _tripRepository.AddAsync(tripNew, cancellationToken);

            return _mapper.Map<TripDto>(tripCreateDto);
        }
        public async Task<TripDto> GetTrip(string tripId, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetOneAsync(c => c.Id == ObjectId.Parse(tripId), cancellationToken);
            return _mapper.Map<TripDto>(trip);
        }
        public async Task<TripDto> UpdateTrip(TripDto tripDto, CancellationToken cancellationToken)
        {
            var serv = await _tripRepository.GetOneAsync(c => c.Id == ObjectId.Parse(tripDto.Id), cancellationToken);

            this._mapper.Map(tripDto, serv);

            var result = await _tripRepository.UpdateTrip(serv, cancellationToken);

            return _mapper.Map<TripDto>(result);
        }
        public async Task<TripDto> DeleteTrip(string tripId, CancellationToken cancellationToken)
        {
            var tripSettings = await _tripRepository.GetOneAsync(c => c.Id == ObjectId.Parse(tripId), cancellationToken);

            if (tripSettings == null)
            {
                throw new Exception("Trip was not found!");
            }

            await _tripRepository.DeleteFromDbAsync(tripSettings, cancellationToken);

            return _mapper.Map<TripDto>(tripSettings);
        }
    }
}
