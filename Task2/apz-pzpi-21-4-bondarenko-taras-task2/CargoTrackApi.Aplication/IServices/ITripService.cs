using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Aplication.IServices
{
    public interface ITripService
    {
        Task<TripDto> AddTripAsync(TripCreateDto dto, CancellationToken cancellationToken);

        Task<TripDto> DeleteTrip(string tripId, CancellationToken cancellationToken);

        Task<TripDto> UpdateTrip(TripDto tripDto, CancellationToken cancellationToken);

        Task<TripDto> GetTrip(string tripId, CancellationToken cancellationToken);
    }
}
