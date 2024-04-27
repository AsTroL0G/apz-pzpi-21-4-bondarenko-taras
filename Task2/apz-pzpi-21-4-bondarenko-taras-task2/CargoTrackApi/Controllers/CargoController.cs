using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{

    [Route("cargo")]
    public class CargoController : BaseController
    {

        private readonly ICargoService _cargoService;

        public CargoController(ICargoService cargo)
        {
            _cargoService = cargo;
        }
        [HttpGet("get/{cargoId}")]
        public async Task<CargoDto> GetCargo(string cargoId, CancellationToken cancellationToken)
        {
            return await _cargoService.GetCargo(cargoId, cancellationToken);
        }
        [HttpGet("get/sender{senderId}")]
        public async Task<List<CargoDto>> GetCargoBySender(string senderId, CancellationToken cancellationToken)
        {
            return await _cargoService.GetCargoBySender(senderId, cancellationToken);
        }
        [HttpGet("get/receiver{receiverId}")]
        public async Task<List<CargoDto>> GetCargoByReceiver(string receiverId, CancellationToken cancellationToken)
        {
            return await _cargoService.GetCargoByReceiver(receiverId, cancellationToken);
        }
        [HttpPost("create")]
        public async Task<ActionResult<CargoDto>> AddCargoAsync([FromBody] CargoCreateDto cargoCreate, CancellationToken cancellationToken)
        {
            await _cargoService.AddCargoAsync(cargoCreate, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<CargoDto>> UpdateCargo([FromBody] CargoDto cargoUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _cargoService.UpdateCargo(cargoUpdateDto, cancellationToken);
            return Ok(result);
        }
        [HttpPut("update/notice")]
        public async Task<ActionResult<CargoDto>> CargoUpdateNotice(string cargoId, string newNoticeId, CancellationToken cancellationToken)
        {
            var result = await _cargoService.CargoUpdateNotice(cargoId, newNoticeId, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("delete/{cargoId}")]
        public async Task<ActionResult<CargoDto>> DeleteCargo(string cargoId, CancellationToken cancellationToken)
        {
            var result = await _cargoService.DeleteCargo(cargoId, cancellationToken);
            return Ok(result);
        }
    }
}
