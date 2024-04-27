using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Aplication.IServices
{
    public interface IDriverService
    {
        Task<DriverDto> AddDriverAsync(DriverCreateDto driverCreateDto, CancellationToken cancellationToken);

        Task<DriverDto> DeleteDriver(string driverId, CancellationToken cancellationToken);

        Task<DriverDto> UpdateDriver(DriverDto dDriverDto, CancellationToken cancellationToken);

        Task<DriverDto> GetDriver(string driverId, CancellationToken cancellationToken);
    }
}
