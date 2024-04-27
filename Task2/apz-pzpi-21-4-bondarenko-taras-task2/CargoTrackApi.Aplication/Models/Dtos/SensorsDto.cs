using CargoTrackApi.Domain.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.Dtos
{

    public class SensorsDto
    {
        public string Id { get; set; }
        public string ContainerId { get; set; }

        public string SensorId { get; set; }
    }

}
