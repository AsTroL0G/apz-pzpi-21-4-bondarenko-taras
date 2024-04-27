using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Paging;
using Microsoft.AspNetCore.Mvc;

namespace CargoTrackApi.Api.Controllers
{
    [Route("container")]
    public class ContainerController : BaseController
    {

        private readonly IContainerService _containerService;

        public ContainerController(IContainerService container)
        {
            _containerService = container;
        }
        [HttpGet]
        public async Task<ActionResult<PagedList<ContainerDto>>> GetUsersPageAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var users = await _containerService.GetContainerPageAsync(pageNumber, pageSize, cancellationToken);
            return Ok(users);
        }
        [HttpGet("find/{coordinatesX}/{coordinatesY}")]
        public async Task<ContainerCoordinatesDto> FindContainer(string coordinatesX, string coordinatesY, CancellationToken cancellationToken)
        {
            return await _containerService.FindContainer(coordinatesX, coordinatesY, cancellationToken);
        }
        [HttpGet("get/{containerId}")]
        public async Task<ContainerDto> GetContainer([FromBody] string containerId, CancellationToken cancellationToken)
        {
            return await _containerService.GetContainer(containerId, cancellationToken);
        }
        [HttpPost("create")]
        public async Task<ActionResult<ContainerDto>> AddContainerAsync([FromBody] ContainerCreateDto containerCreate, CancellationToken cancellationToken)
        {
            await _containerService.AddContainerAsync(containerCreate, cancellationToken);
            return Ok();
        }


        [HttpPut("update")]
        public async Task<ActionResult<ContainerDto>> UpdateContainer([FromBody] ContainerDto containerUpdateDto, CancellationToken cancellationToken)
        {
            var result = await _containerService.UpdateContainer(containerUpdateDto, cancellationToken);
            return Ok(result);
        }
        [HttpDelete("delete/{containerId}")]
        public async Task<ActionResult<ContainerDto>> DeleteContainer(string containerId, CancellationToken cancellationToken)
        {
            var result = await _containerService.DeleteContainer(containerId, cancellationToken);
            return Ok(result);
        }

    }
}
