using CargoTrackApi.Domain.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Domain.Entities
{
    public class Schedule : EntityBase
    {
        public ObjectId TripId { get; set; }
        public ObjectId CityId { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
