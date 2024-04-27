using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{

    [Route("schedule")]
    public class ScheduleController : BaseController
    {

        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService schedule)
        {
            _scheduleService = schedule;
        }
        [HttpGet("get/{scheduleId}")]
        public async Task<ScheduleDto> GetSchedule(string scheduleId, CancellationToken cancellationToken)
        {
            return await _scheduleService.GetSchedule(scheduleId, cancellationToken);
        }
        [HttpGet("trip/{tripId}")]
        public async Task<List<ScheduleDto>> GetScheduleByTripId(string tripId, CancellationToken cancellationToken)
        {
            return await _scheduleService.GetScheduleByTripId(tripId, cancellationToken);
        }

        [HttpPost("create")]
        public async Task<ActionResult<ScheduleDto>> AddScheduleAsync([FromBody] ScheduleCreateDto scheduleCreate, CancellationToken cancellationToken)
        {
            await _scheduleService.AddScheduleAsync(scheduleCreate, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<ScheduleDto>> UpdateSchedule([FromBody] ScheduleDto scheduleUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _scheduleService.UpdateSchedule(scheduleUpdateDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{scheduleId}")]
        public async Task<ActionResult<ScheduleDto>> DeleteSchedule(string scheduleId, CancellationToken cancellationToken)
        {
            var result = await _scheduleService.DeleteSchedule(scheduleId, cancellationToken);
            return Ok(result);
        }
    }
}
