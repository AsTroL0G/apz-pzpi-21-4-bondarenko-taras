using CargoTrackApi.Domain.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Domain.Entities
{
    public class Value : EntityBase
    {
        public ObjectId SensorId { get; set; }

        public string Values { get; set; }

        public DateTime? MeasurementTime { get; set; }

    }
}
