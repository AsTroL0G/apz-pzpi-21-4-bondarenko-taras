using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Aplication.IServices
{
    public interface ICityService
    {
        Task<CityDto> GetCity(string cityId, CancellationToken cancellationToken);
        Task<CityDto> AddCityAsync(CityCreateDto cityCreateDto, CancellationToken cancellationToken);

        Task<CityDto> DeleteCity(string cityId, CancellationToken cancellationToken);

        Task<CityDto> UpdateCity(CityDto cityDto, CancellationToken cancellationToken);
    }
}
