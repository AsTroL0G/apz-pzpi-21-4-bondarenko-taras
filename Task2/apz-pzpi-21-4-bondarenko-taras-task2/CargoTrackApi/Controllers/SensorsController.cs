using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{

    [Route("sensors")]
    public class SensorsController : BaseController
    {

        private readonly ISensorsService _sensorsService;

        public SensorsController(ISensorsService sensors)
        {
            _sensorsService = sensors;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSensors(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var result = await _sensorsService.GetAllSensors(pageNumber, pageSize, cancellationToken);
            return Ok(result);
        }
        [HttpGet("get/{type}/container/{containerId}")]
        public async Task<SensorsDto> GetSensorByType(string type, string containerId, CancellationToken cancellationToken)
        {
            return await _sensorsService.GetSensorsByType(type, containerId, cancellationToken);
        }
        [HttpGet("get/container/{containerId}")]
        public async Task<List<SensorsDto>> GetSensorsById(string containerId, CancellationToken cancellationToken)
        {
            return await _sensorsService.GetSensorsById(containerId, cancellationToken);
        }
        [HttpPost("create")]
        public async Task<ActionResult<SensorsDto>> AddSensorsAsync([FromBody] SensorsCreateDto create, CancellationToken cancellationToken)
        {
            await _sensorsService.AddSensorsAsync(create, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<SensorsDto>> UpdateSensors([FromBody] SensorsDto valueUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _sensorsService.UpdateSensors(valueUpdateDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{sensorsId}")]
        public async Task<ActionResult<SensorsDto>> DeleteSensors(string sensorsId, CancellationToken cancellationToken)
        {
            var result = await _sensorsService.DeleteSensors(sensorsId, cancellationToken);
            return Ok(result);
        }
    }
}
