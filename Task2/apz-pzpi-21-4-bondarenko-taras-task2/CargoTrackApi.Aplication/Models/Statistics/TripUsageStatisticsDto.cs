using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.Statistics
{
    public class TripUsageStatisticsDto
    {
        public string TripId { get; set; }
        public string ContainerName { get; set; }

        public string ContainerType { get; set; }

        public float TotalWeight{ get; set; }

        public double AverageSpeed { get; set; }

        public string TimeSpent { get; set; }

    }
}
