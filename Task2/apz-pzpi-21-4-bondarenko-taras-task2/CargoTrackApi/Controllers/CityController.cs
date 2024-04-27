using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{
    [Route("city")]
    public class CityController : BaseController
    {

        private readonly ICityService _cityService;

        public CityController(ICityService city)
        {
            _cityService = city;
        }

        [HttpGet("get/{cityId}")]
        public async Task<CityDto> GetCity(string cityId, CancellationToken cancellationToken)
        {
            return await _cityService.GetCity(cityId, cancellationToken);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CityDto>> AddCarAsync([FromBody] CityCreateDto cityCreate, CancellationToken cancellationToken)
        {
            await _cityService.AddCityAsync(cityCreate, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<CityDto>> UpdateCity([FromBody] CityDto cityUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _cityService.UpdateCity(cityUpdateDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{cityId}")]
        public async Task<ActionResult<CityDto>> DeleteCity(string cityId, CancellationToken cancellationToken)
        {
            var result = await _cityService.DeleteCity(cityId, cancellationToken);
            return Ok(result);
        }
    }
}
