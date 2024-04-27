using CargoTrackApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{
    public interface INoticeRepository : IBaseRepository<Notice>
    {
        Task<Notice> UpdateNotice(Notice notice, CancellationToken cancellationToken);

        Task<Notice> GetContainer(string tripId, CancellationToken cancellationToken);
        Task<List<Notice>> GetTripByContainerId(string containerId, CancellationToken cancellationToken);

    }
}
