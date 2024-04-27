using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.Statistics
{
    public class ContainerUsageStatisticsDto
    {
        public string ContainerId { get; set; }

        public string ContainerName { get; set; }

        public string ContainerType { get; set; }

        public int NumberTrips { get; set; }

        public double AverageLoadCapacity { get; set; }

        public double VolumetricWeight { get; set; }

    }
}
