using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Aplication.IServices
{
    public interface ICarService
    {
        Task<CarDto> AddCarAsync(CarCreateDto carCreateDto, CancellationToken cancellationToken);

        Task<CarDto> DeleteCar(string carId, CancellationToken cancellationToken);

        Task<CarDto> UpdateCar(CarDto carDto, CancellationToken cancellationToken);

        Task<CarDto> GetCar(string carId, CancellationToken cancellationToken);
    }
}
