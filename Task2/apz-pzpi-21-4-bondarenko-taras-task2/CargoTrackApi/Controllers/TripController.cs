using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{
    [Route("trip")]
    public class TripController : BaseController
    {

        private readonly ITripService _tripService;

        public TripController(ITripService trip)
        {
            _tripService = trip;
        }

        [HttpGet("get/{tripId}")]
        public async Task<TripDto> GetTrip(string tripId, CancellationToken cancellationToken)
        {
            return await _tripService.GetTrip(tripId, cancellationToken);
        }

        [HttpPost("create")]
        public async Task<ActionResult<TripDto>> AddTripAsync([FromBody] TripCreateDto tripCreate, CancellationToken cancellationToken)
        {
            await _tripService.AddTripAsync(tripCreate, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<TripDto>> UpdateTrip([FromBody] TripDto tripUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _tripService.UpdateTrip(tripUpdateDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{tripId}")]
        public async Task<ActionResult<TripDto>> DeleteTrip(string tripId, CancellationToken cancellationToken)
        {
            var result = await _tripService.DeleteTrip(tripId, cancellationToken);
            return Ok(result);
        }
    }
}
