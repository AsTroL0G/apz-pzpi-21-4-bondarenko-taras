using AutoMapper;
using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Paging;
using CargoTrackApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Infrastructure.Services
{
    public class ContainerService : IContainerService
    {
        private readonly IContainerRepository _containerRepository;
        private readonly ISensorsRepository _sensorsRepository;
        private readonly ISensorRepository _sensorRepository;
        private readonly IValueRepository _valueRepository;
        private readonly IMapper _mapper;


        public ContainerService(IContainerRepository containerRepository, ISensorsRepository sensorsRepository, ISensorRepository sensorRepository, IValueRepository valueRepository, IMapper mapper)
        {
            _containerRepository = containerRepository;
            _sensorsRepository = sensorsRepository;
            _sensorRepository = sensorRepository;
            _valueRepository = valueRepository;
            _mapper = mapper;
        }

        public async Task<ContainerDto> AddContainerAsync(ContainerCreateDto containerCreateDto, CancellationToken cancellationToken)
        {
            var containerNew = new Container()
            {
                Type = containerCreateDto.Type,
                Name = containerCreateDto.Name,
                LoadCapacity = containerCreateDto.LoadCapacity,
                Length = containerCreateDto.Length,
                Height = containerCreateDto.Height,
                Width = containerCreateDto.Width,
                Status = containerCreateDto.Status,
            };

            await _containerRepository.AddAsync(containerNew, cancellationToken);

            return _mapper.Map<ContainerDto>(containerCreateDto);
        }

        public async Task<ContainerDto> UpdateContainer(ContainerDto containerDto, CancellationToken cancellationToken)
        {
            var serv = await _containerRepository.GetOneAsync(c => c.Id == ObjectId.Parse(containerDto.Id), cancellationToken);

            this._mapper.Map(containerDto, serv);

            var result = await _containerRepository.UpdateContainer(serv, cancellationToken);

            return _mapper.Map<ContainerDto>(result);
        }
        public async Task<ContainerDto> DeleteContainer(string containerId, CancellationToken cancellationToken)
        {
            var containerSettings = await _containerRepository.GetOneAsync(c => c.Id == ObjectId.Parse(containerId), cancellationToken);

            if (containerSettings == null)
            {
                throw new Exception("Container was not found!");
            }

            await _containerRepository.DeleteFromDbAsync(containerSettings, cancellationToken);

            return _mapper.Map<ContainerDto>(containerSettings);
        }

        public async Task<ContainerDto> GetContainer(string containerId, CancellationToken cancellationToken)
        {
            var car = await _containerRepository.GetOneAsync(c => c.Id == ObjectId.Parse(containerId), cancellationToken);
            return _mapper.Map<ContainerDto>(car);
        }
        public async Task<PagedList<ContainerDto>> GetContainerPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var entities = await _containerRepository.GetPageAsync(pageNumber, pageSize, cancellationToken);
            var dtos = _mapper.Map<List<ContainerDto>>(entities);
            var count = await _containerRepository.GetTotalCountAsync(cancellationToken);
            return new PagedList<ContainerDto>(dtos, pageNumber, pageSize, count);
        }
        public async Task<ContainerCoordinatesDto> FindContainer(string coordinatesX, string coordinatesY, CancellationToken cancellationToken)
        {
            var sensorGPS = await _sensorRepository.GetSensorByType("GPS",cancellationToken);
            var sensors = await _sensorsRepository.FindSensorsByGPS(sensorGPS,cancellationToken);
            var  values = await _valueRepository.GetLatestValueBySensorId(sensors, cancellationToken);

            // Початкові координати
            double initialLat = Convert.ToDouble(coordinatesX);
            double initialLon = Convert.ToDouble(coordinatesY);

            var nearestContainer = new ContainerCoordinatesDto();
            double minDist = double.MaxValue;

            foreach (var value in values)
            {
                string[] coordinatesContainer = value.Coordinates.Split('/');

                double containerLat = Convert.ToDouble(coordinatesContainer[0]);
                double containerLon = Convert.ToDouble(coordinatesContainer[1]);

                double dist = CalculateDistance(initialLat, initialLon, containerLat, containerLon);

                if (dist< minDist)
                {
                    minDist = dist;
                    nearestContainer.ContainerId = value.ContainerId;
                    nearestContainer.Coordinates = value.Coordinates;
                    nearestContainer.Distance = minDist.ToString();
                }
            }

            return nearestContainer;
        }

        static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Константа радіусу Землі в кілометрах
            const double R = 6371;

            // Перетворення координат у радіани
            lat1 = lat1 * Math.PI / 180;
            lon1 = lon1 * Math.PI / 180;
            lat2 = lat2 * Math.PI / 180;
            lon2 = lon2 * Math.PI / 180;


            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;

            // Формула гаверсинусів
            double a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = R * c;

            return distance;
        }
    }
}
