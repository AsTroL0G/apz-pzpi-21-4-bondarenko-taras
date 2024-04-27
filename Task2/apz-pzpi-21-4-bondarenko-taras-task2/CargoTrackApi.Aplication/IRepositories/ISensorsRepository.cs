using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{

    public interface ISensorsRepository : IBaseRepository<Sensors>
    {
        Task<Sensors> UpdateSensors(Sensors sensors, CancellationToken cancellationToken);

        Task<List<Sensors>> FindSensorsByGPS(List<Sensor> sensorIds, CancellationToken cancellationToken);

        Task<Sensors> GetSensorsByType(List<Sensors> sensorList, string containerId, CancellationToken cancellationToken);
        Task<List<Sensors>> GetSensorsById(string containerId, CancellationToken cancellationToken);

    }
}
