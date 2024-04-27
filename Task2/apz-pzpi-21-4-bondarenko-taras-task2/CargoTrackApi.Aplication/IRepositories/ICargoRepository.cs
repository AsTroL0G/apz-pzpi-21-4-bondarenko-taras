using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{

    public interface ICargoRepository : IBaseRepository<Cargo>
    {
        Task<Cargo> UpdateCargo(Cargo cargo, CancellationToken cancellationToken);

        Task<bool> CargoUpdateNotice(string cargoId, string newNoticeId, CancellationToken cancellationToken);

        Task<List<Cargo>> GetCargoBySender(string senderId, CancellationToken cancellationToken);

        Task<List<Cargo>> GetCargoByReceiver(string receiverId, CancellationToken cancellationToken);

        Task<List<Cargo>> GetCargoByNotice(string noticeId, CancellationToken cancellationToken);
    }
}
