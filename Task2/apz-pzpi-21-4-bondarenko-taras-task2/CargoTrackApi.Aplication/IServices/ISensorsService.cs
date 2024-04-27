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
    public interface ISensorsService
    {

        Task<SensorsDto> AddSensorsAsync(SensorsCreateDto dto, CancellationToken cancellationToken);

        Task<SensorsDto> DeleteSensors(string valueId, CancellationToken cancellationToken);

        Task<SensorsDto> UpdateSensors(SensorsDto sensorDto, CancellationToken cancellationToken);

        Task<SensorsDto> GetSensorsByType(string type, string containerId, CancellationToken cancellationToken);
        Task<List<SensorsDto>> GetSensorsById(string containerId, CancellationToken cancellationToken);

        Task<PagedList<SensorsDto>> GetAllSensors(int pageNumber, int pageSize, CancellationToken cancellationToken);

    }
}
