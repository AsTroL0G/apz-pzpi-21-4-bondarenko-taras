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
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository _cargoRepository;

        private readonly IMapper _mapper;


        public CargoService(ICargoRepository cargoRepository, IMapper mapper)
        {
            _cargoRepository = cargoRepository;
            _mapper = mapper;
        }


        public async Task<CargoDto> AddCargoAsync(CargoCreateDto cargoCreateDto, CancellationToken cancellationToken)
        {

            float shippingPrice;
            var factWeight = cargoCreateDto.Weight;
            var volumetricWeight = (cargoCreateDto.Length * cargoCreateDto.Height * cargoCreateDto.Width) / 4000;
            if(volumetricWeight> factWeight)
            {
                shippingPrice = volumetricWeight * 7;
            }
            else
            {
                shippingPrice = volumetricWeight * 10;
            }
            var cargoNew = new Cargo()
            {

                Name = cargoCreateDto.Name,

                Weight = cargoCreateDto.Weight,

                Length = cargoCreateDto.Length,
                Height = cargoCreateDto.Height,
                Width = cargoCreateDto.Width,
                AnnouncedPrice = cargoCreateDto.AnnouncedPrice,
                ShippingPrice = shippingPrice,
                SenderId = ObjectId.Parse(cargoCreateDto.SenderId),
                ReceiverId = ObjectId.Parse(cargoCreateDto.ReceiverId),
                CitySenderId = ObjectId.Parse(cargoCreateDto.CitySenderId),
                AddressSenderId = cargoCreateDto.AddressSenderId,
                CityReceiverId = ObjectId.Parse(cargoCreateDto.CityReceiverId),
                AddressReceiverId = cargoCreateDto.AddressReceiverId,

             };

            await _cargoRepository.AddAsync(cargoNew, cancellationToken);

            return _mapper.Map<CargoDto>(cargoCreateDto);
        }

        public async Task<CargoDto> UpdateCargo(CargoDto cargoDto, CancellationToken cancellationToken)
        {
            var serv = await _cargoRepository.GetOneAsync(c => c.Id == ObjectId.Parse(cargoDto.Id), cancellationToken);

            this._mapper.Map(cargoDto, serv);

            var result = await _cargoRepository.UpdateCargo(serv, cancellationToken);

            return _mapper.Map<CargoDto>(result);
        }
        public async Task<CargoDto> DeleteCargo(string cargoId, CancellationToken cancellationToken)
        {
            var cargoSettings = await _cargoRepository.GetOneAsync(c => c.Id == ObjectId.Parse(cargoId), cancellationToken);

            if (cargoSettings == null)
            {
                throw new Exception("Cargo was not found!");
            }

            await _cargoRepository.DeleteFromDbAsync(cargoSettings, cancellationToken);

            return _mapper.Map<CargoDto>(cargoSettings);
        }

        public async Task<CargoDto> GetCargo(string cargoId, CancellationToken cancellationToken)
        {
            var car = await _cargoRepository.GetOneAsync(c => c.Id == ObjectId.Parse(cargoId), cancellationToken);
            return _mapper.Map<CargoDto>(car);
        }

        public async Task<List<CargoDto>> GetCargoBySender(string senderId, CancellationToken cancellationToken)
        {
            var cargo = await _cargoRepository.GetCargoBySender(senderId, cancellationToken);
            return _mapper.Map<List<CargoDto>>(cargo);
        }

        public async Task<List<CargoDto>> GetCargoByReceiver(string receiverId, CancellationToken cancellationToken)
        {
            var cargo = await _cargoRepository.GetCargoByReceiver(receiverId, cancellationToken);
            return _mapper.Map<List<CargoDto>>(cargo);

        }
        public async Task<CargoDto> CargoUpdateNotice(string cargoId, string newNoticeId, CancellationToken cancellationToken)
        {

            await _cargoRepository.CargoUpdateNotice(cargoId, newNoticeId, cancellationToken);

            var cargo = await  GetCargo(cargoId, cancellationToken);

            return _mapper.Map<CargoDto>(cargo);
        }

    }
}
