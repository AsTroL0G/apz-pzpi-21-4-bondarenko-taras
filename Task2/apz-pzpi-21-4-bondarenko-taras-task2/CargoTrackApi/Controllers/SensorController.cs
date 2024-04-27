using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{
    [Route("sensor")]
    public class SensorController : BaseController
    {

        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSensor(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var result = await _sensorService.GetSensorPages(pageNumber, pageSize, cancellationToken);
            return Ok(result);
        }
        [HttpGet("get/{type}")]
        public async Task<List<SensorDto>> GetSensorByType(string type, CancellationToken cancellationToken)
        {
            return await _sensorService.GetSensorByType(type, cancellationToken);
        }

        [HttpPost("create")]
        public async Task<ActionResult<SensorDto>> AddSensorAsync([FromBody] SensorCreateDto create, CancellationToken cancellationToken)
        {
            await _sensorService.AddSensorAsync(create, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<SensorDto>> UpdateSensor([FromBody] SensorDto moderUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _sensorService.UpdateSensor(moderUpdateDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{sensorId}")]
        public async Task<ActionResult<SensorDto>> DeleteSensor(string sensorId, CancellationToken cancellationToken)
        {
            var result = await _sensorService.DeleteSensor(sensorId, cancellationToken);
            return Ok(result);
        }
    }
}
