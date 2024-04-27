using CargoTrackApi.Application.Models;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IServices
{
    public interface IValueService
    {

        Task<ValueDto> AddValueAsync(ValueCreateDto dto, CancellationToken cancellationToken);

        Task<ValueDto> DeleteValue(string valueId, CancellationToken cancellationToken);

        Task<ValueDto> UpdateValue(ValueDto sensorDto, CancellationToken cancellationToken);

        Task<List<ValueDto>> GetValueBySensorId(string sensorId, CancellationToken cancellationToken);
    }

}
