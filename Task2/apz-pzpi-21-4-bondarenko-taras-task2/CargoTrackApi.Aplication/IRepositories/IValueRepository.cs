using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{
    public interface IValueRepository : IBaseRepository<Value>
    {
        Task<Value> UpdateValue(Value value, CancellationToken cancellationToken);

        Task<List<Value>> GetValueBySensorId(string sensorId, CancellationToken cancellationToken);

        Task<List<ContainerCoordinatesDto>> GetLatestValueBySensorId(List<Sensors> sensorId, CancellationToken cancellationToken);

        Task<List<ContainerCoordinatesDto>> GetNewestValueBySensorId(List<Sensors> sensorId, CancellationToken cancellationToken);

    }
}
