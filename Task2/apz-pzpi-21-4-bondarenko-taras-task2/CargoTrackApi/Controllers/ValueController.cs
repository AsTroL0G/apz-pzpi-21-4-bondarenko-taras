using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.Models;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{

    [Route("value")]
    public class ValueController : BaseController
    {

        private readonly IValueService _valueService;

        public ValueController(IValueService value)
        {
            _valueService = value;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ValueDto>> AddValueAsync([FromBody] ValueCreateDto create, CancellationToken cancellationToken)
        {
            await _valueService.AddValueAsync(create, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<ValueDto>> UpdateSettings([FromBody] ValueDto valueUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _valueService.UpdateValue(valueUpdateDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{valueId}")]
        public async Task<ActionResult<ValueDto>> DeleteModeSettings(string valueId, CancellationToken cancellationToken)
        {
            var result = await _valueService.DeleteValue(valueId, cancellationToken);
            return Ok(result);
        }
        [HttpGet("getBySensorId/{sensorId}")]
        public async Task<List<ValueDto>> GetModeSettingsByModeId(string sensorId, CancellationToken cancellationToken)
        {
            return await _valueService.GetValueBySensorId(sensorId, cancellationToken);
        }

    }
}
