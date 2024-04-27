using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.Dtos
{
    public class ValueDto
    {
        public string Id { get; set; }
        public string SensorId { get; set; }

        public string? Values { get; set; }

        public DateTime? MeasurementTime { get; set; }
    }
}
