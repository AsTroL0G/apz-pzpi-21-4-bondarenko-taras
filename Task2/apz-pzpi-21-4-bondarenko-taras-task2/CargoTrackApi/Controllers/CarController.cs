using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{
    [Route("car")]
    public class CarController : BaseController
    {

        private readonly ICarService _carService;

        public CarController(ICarService car)
        {
            _carService = car;
        }

        [HttpGet("get/{carId}")]
        public async Task<CarDto> GetCar(string carId, CancellationToken cancellationToken)
        {
            return await _carService.GetCar(carId, cancellationToken);
        }

        [HttpPost("create")]
        public async Task<ActionResult<CarDto>> AddCarAsync([FromBody] CarCreateDto carCreate, CancellationToken cancellationToken)
        {
            await _carService.AddCarAsync(carCreate, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<CarDto>> UpdateCar([FromBody] CarDto carUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _carService.UpdateCar(carUpdateDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{carId}")]
        public async Task<ActionResult<CarDto>> DeleteCar(string carId, CancellationToken cancellationToken)
        {
            var result = await _carService.DeleteCar(carId, cancellationToken);
            return Ok(result);
        }
    }
}
