using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Aplication.IServices
{
    public interface IContainerService
    {
        Task<ContainerDto> AddContainerAsync(ContainerCreateDto containerCreateDto, CancellationToken cancellationToken);

        Task<ContainerDto> DeleteContainer(string containerId, CancellationToken cancellationToken);

        Task<ContainerDto> UpdateContainer(ContainerDto containerDto, CancellationToken cancellationToken);

        Task<ContainerDto> GetContainer(string containerId, CancellationToken cancellationToken);

        Task<PagedList<ContainerDto>> GetContainerPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<ContainerCoordinatesDto> FindContainer(string coordinatesX, string coordinatesY, CancellationToken cancellationToken);

        
    }
}
