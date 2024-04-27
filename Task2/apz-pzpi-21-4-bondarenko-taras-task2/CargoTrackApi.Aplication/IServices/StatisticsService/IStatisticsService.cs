using CargoTrackApi.Application.Models;
using CargoTrackApi.Application.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IServices.StatisticsService
{
    public interface IStatisticsService
    {
        Task<ContainerUsageStatisticsDto> ContainerUsageStatistics(string containerId, CancellationToken cancellationToken);

        Task<TripUsageStatisticsDto> TripUsageStatistics(string tripId, CancellationToken cancellationToken);

    }
}
