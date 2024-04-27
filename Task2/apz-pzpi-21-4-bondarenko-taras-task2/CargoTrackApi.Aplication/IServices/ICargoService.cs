using CargoTrackApi.Application.Models.CreateDtos;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IServices
{
    public interface ICargoService
    {
        Task<CargoDto> AddCargoAsync(CargoCreateDto cargoCreateDto, CancellationToken cancellationToken);

        Task<CargoDto> DeleteCargo(string cargoId, CancellationToken cancellationToken);

        Task<CargoDto> UpdateCargo(CargoDto cargoDto, CancellationToken cancellationToken);

        Task<CargoDto> GetCargo(string cargoId, CancellationToken cancellationToken);

        Task<CargoDto> CargoUpdateNotice(string noticeId, string newContainerId, CancellationToken cancellationToken);

        Task<List<CargoDto>> GetCargoBySender(string senderId, CancellationToken cancellationToken);

        Task<List<CargoDto>> GetCargoByReceiver(string receiverId, CancellationToken cancellationToken);

    }
}
