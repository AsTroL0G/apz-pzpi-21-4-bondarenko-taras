using CargoTrackApi.Application.Models.Statistics;
using MongoDB.Bson;
using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.IServices.StatisticsService;
using CargoTrackApi.Domain.Entities;
using System.ComponentModel;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Models.CreateDtos;


namespace CargoTrackApi.Infrastructure.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IContainerRepository _containerRepository;

        private readonly ITripRepository _tripRepository;

        private readonly IScheduleRepository _scheduleRepository;

        private readonly ICargoRepository _cargoRepository;

        private readonly ISensorsRepository _sensorsRepository;

        private readonly ISensorRepository _sensorRepository;

        private readonly IValueRepository _valueRepository;

        private readonly INoticeRepository _noticeRepository;


        public StatisticsService(IContainerRepository containerRepository, INoticeRepository noticeRepository, ICargoRepository cargoRepository, ITripRepository tripRepository, ISensorsRepository sensorsRepository, IValueRepository valueRepository, IScheduleRepository scheduleRepository, ISensorRepository sensorRepository)
        {
            _containerRepository = containerRepository;
            _tripRepository = tripRepository;
            _sensorsRepository = sensorsRepository;
            _valueRepository = valueRepository;
            _scheduleRepository = scheduleRepository;
            _cargoRepository = cargoRepository;
            _sensorRepository = sensorRepository;
            _noticeRepository = noticeRepository;
        }

        public async Task<ContainerUsageStatisticsDto> ContainerUsageStatistics(string containerId, CancellationToken cancellationToken)
        {
            var containerUsageStatisticsDto = new ContainerUsageStatisticsDto();

            var container = await _containerRepository.GetOneAsync(c => c.Id == ObjectId.Parse(containerId), cancellationToken);
            containerUsageStatisticsDto.ContainerId = container.Id.ToString();
            containerUsageStatisticsDto.ContainerName = container.Name;
            containerUsageStatisticsDto.ContainerType = container.Type;

            var numberTrip = await _noticeRepository.GetTripByContainerId(containerId, cancellationToken);
            containerUsageStatisticsDto.NumberTrips = numberTrip.Count;

            var cargos = await _cargoRepository.GetCargoByNotice(numberTrip[0].Id.ToString(), cancellationToken);

            double cargoLength=0;
            double cargoHeight=0;
            double cargoWidth=0;
            foreach (var cargo in cargos)
            {

                containerUsageStatisticsDto.AverageLoadCapacity += cargo.Weight;
            }

            containerUsageStatisticsDto.AverageLoadCapacity = containerUsageStatisticsDto.AverageLoadCapacity/containerUsageStatisticsDto.NumberTrips;

            foreach (var cargo in cargos)
            {

                cargoLength += cargo.Length;
                cargoHeight += cargo.Height;
                cargoWidth += cargo.Width;
            }

            var volumetricWeight = (cargoLength * cargoHeight * cargoWidth) / 4000;

            containerUsageStatisticsDto.VolumetricWeight = volumetricWeight / containerUsageStatisticsDto.NumberTrips;

            return containerUsageStatisticsDto;
        }


        public async Task <TripUsageStatisticsDto> TripUsageStatistics(string tripId, CancellationToken cancellationToken)
        {
            var tripUsageStatistics = new TripUsageStatisticsDto();

            var trip = await _tripRepository.GetOneAsync(c => c.Id == ObjectId.Parse(tripId), cancellationToken);

            var notice = await _noticeRepository.GetContainer(tripId, cancellationToken);

            var container = await _containerRepository.GetOneAsync(c => c.Id == notice.ContainerId, cancellationToken);

            tripUsageStatistics.TripId = tripId;
            tripUsageStatistics.ContainerName = container.Name;
            tripUsageStatistics.ContainerType = container.Type;
            double time = 2;

            var cargos = await _cargoRepository.GetCargoByNotice(notice.Id.ToString(), cancellationToken);

            foreach (var cargo in cargos)
            {

                tripUsageStatistics.TotalWeight += cargo.Weight;
            }


            var sensorGPS = await _sensorRepository.GetSensorByType("GPS", cancellationToken);
            var sensors = await _sensorsRepository.FindSensorsByGPS(sensorGPS, cancellationToken);
            var values = await _valueRepository.GetLatestValueBySensorId(sensors, cancellationToken);
            var valuesnew = await _valueRepository.GetNewestValueBySensorId(sensors, cancellationToken);

            double minDist = double.MaxValue;

            double containerLat = double.MaxValue;
            double containerLon = double.MaxValue;

            double containerLat1 = double.MaxValue;
            double containerLon1 = double.MaxValue;
                foreach (var value in values)
                {

                    if (value.ContainerId == (container.Id).ToString()) {

                        string[] coordinatesContainer = value.Coordinates.Split('/');

                         containerLat = Convert.ToDouble(coordinatesContainer[0]);
                         containerLon = Convert.ToDouble(coordinatesContainer[1]);


                    }
                }
                foreach (var value in valuesnew)
                {

                    if (value.ContainerId == (container.Id).ToString())
                    {

                        string[] coordinatesContainer = value.Coordinates.Split('/');

                        containerLat1 = Convert.ToDouble(coordinatesContainer[0]);
                        containerLon1 = Convert.ToDouble(coordinatesContainer[1]);


                    }
                }


            double dist = CalculateDistance(containerLat1, containerLon1, containerLat, containerLon);

            tripUsageStatistics.AverageSpeed = (dist/time);


            tripUsageStatistics.TimeSpent = "20:33:00";


            return tripUsageStatistics;
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

            // Різниця між широтами та довготами
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;

            // Формула гаверсинусів
            double a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // Відстань у кілометрах
            double distance = R * c;

            return distance;
        }
    }


    
}
    


