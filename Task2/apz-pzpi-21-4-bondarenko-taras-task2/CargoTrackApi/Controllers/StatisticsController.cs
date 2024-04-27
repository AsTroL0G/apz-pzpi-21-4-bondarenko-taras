using Microsoft.AspNetCore.Mvc;
using CargoTrackApi.Application.IServices.StatisticsService;
using CargoTrackApi.Application.Models.Statistics;
using System.Threading;

namespace CargoTrackApi.Api.Controllers
{

    [Route("statistics")]
    public class StatisticsController : BaseController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("delivery/container/{containerId}")]
        public async Task<ActionResult<ContainerUsageStatisticsDto>> ContainerUsageStatistics(string containerId, CancellationToken cancellationToken)
        {
            var result = await _statisticsService.ContainerUsageStatistics(containerId, cancellationToken);
            return result;
        }

        [HttpGet("trip/{tripId}")]
        public async Task<TripUsageStatisticsDto> TripUsageStatistics(string tripId, CancellationToken cancellationToken)
        {
            var result = await _statisticsService.TripUsageStatistics(tripId, cancellationToken);
            return result;
        }
    }
}
