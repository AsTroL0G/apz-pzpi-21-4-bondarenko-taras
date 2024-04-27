using AutoMapper;
using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.IServices.Identity;
using CargoTrackApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        private readonly IMapper _mapper;

        private readonly IPasswordHasher _passwordHasher;

        public DriverService(IDriverRepository driverRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _driverRepository = driverRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public async Task<DriverDto> AddDriverAsync(DriverCreateDto driverCreateDto, CancellationToken cancellationToken)
        {


            var fdg = driverCreateDto.FirstName;

            var driverNew = new Driver
            { 
                FirstName = driverCreateDto.FirstName,
                LastName = driverCreateDto.LastName,
                Patronym = driverCreateDto.Patronym,
                Email = driverCreateDto.Email,
                PhoneNumber = driverCreateDto.PhoneNumber,
                DriverLicenseNumber = driverCreateDto.DriverLicenseNumber,
                PasswordHash = this._passwordHasher.Hash(driverCreateDto.PasswordHash),
                CreatedDateUtc = DateTime.UtcNow,
                CreatedById = ObjectId.Empty,
            };

            await _driverRepository.AddAsync(driverNew, cancellationToken);

            return _mapper.Map<DriverDto>(driverNew);

        }

        public async Task<DriverDto> UpdateDriver(DriverDto driverDto, CancellationToken cancellationToken)
        {
            var serv = await _driverRepository.GetOneAsync(c => c.Id == ObjectId.Parse(driverDto.Id), cancellationToken);

            this._mapper.Map(driverDto, serv);

            var result = await _driverRepository.UpdateDriver(serv, cancellationToken);

            return _mapper.Map<DriverDto>(result);
        }
        public async Task<DriverDto> DeleteDriver(string driverId, CancellationToken cancellationToken)
        {
            var driverSettings = await _driverRepository.GetOneAsync(c => c.Id == ObjectId.Parse(driverId), cancellationToken);

            if (driverSettings == null)
            {
                throw new Exception("Driver was not found!");
            }

            await _driverRepository.DeleteFromDbAsync(driverSettings, cancellationToken);

            return _mapper.Map<DriverDto>(driverSettings);
        }
        public async Task<DriverDto> GetDriver(string driverId, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetOneAsync(c => c.Id == ObjectId.Parse(driverId), cancellationToken);
            return _mapper.Map<DriverDto>(driver);
        }
    }
}
