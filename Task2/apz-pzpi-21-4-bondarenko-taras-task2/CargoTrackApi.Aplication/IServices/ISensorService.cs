using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Paging;
using CargoTrackApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IServices
{
    public interface ISensorService
    {
        Task<SensorDto> AddSensorAsync(SensorCreateDto dto, CancellationToken cancellationToken);

        Task<PagedList<SensorDto>> GetSensorPages(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<SensorDto> DeleteSensor(string sensorId, CancellationToken cancellationToken);

        Task<SensorDto> UpdateSensor(SensorDto sensorDto, CancellationToken cancellationToken);
        Task<List<SensorDto>> GetSensorByType(string type, CancellationToken cancellationToken);

    }
}
