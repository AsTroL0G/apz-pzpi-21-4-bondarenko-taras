using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{

    public interface IScheduleRepository : IBaseRepository<Schedule>
    {
        Task<Schedule> UpdateSchedule(Schedule schedule, CancellationToken cancellationToken);
        Task<List<Schedule>> GetScheduleByTripId(string tripId, CancellationToken cancellationToken);

    }
}
