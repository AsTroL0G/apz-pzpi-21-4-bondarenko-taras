using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{

    [Route("driver")]
    public class DriverController : BaseController
    {

        private readonly IDriverService _driverService;

        public DriverController(IDriverService driver)
        {
            _driverService = driver;
        }
        [HttpGet("get/{driverId}")]
        public async Task<DriverDto> GetDriver(string driverId, CancellationToken cancellationToken)
        {
            return await _driverService.GetDriver(driverId, cancellationToken);
        }

        [HttpPost("create")]
        public async Task<ActionResult<DriverDto>> AddCarAsync([FromBody] DriverCreateDto driverCreate, CancellationToken cancellationToken)
        {
            await _driverService.AddDriverAsync(driverCreate, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<DriverDto>> UpdateContainer([FromBody] DriverDto driverUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _driverService.UpdateDriver(driverUpdateDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{driverId}")]
        public async Task<ActionResult<DriverDto>> DeleteContainer(string driverId, CancellationToken cancellationToken)
        {
            var result = await _driverService.DeleteDriver(driverId, cancellationToken);
            return Ok(result);
        }
    }
}
