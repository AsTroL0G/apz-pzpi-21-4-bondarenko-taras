using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.CreateDtos
{
    public class ValueCreateDto
    {
        public string SensorId { get; set; }

        public string? Values { get; set; }

        public DateTime? MeasurementTime { get; set; }
    }
}
