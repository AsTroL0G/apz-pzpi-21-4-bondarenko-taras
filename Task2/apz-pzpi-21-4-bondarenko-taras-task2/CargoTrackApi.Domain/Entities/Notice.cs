using CargoTrackApi.Domain.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Domain.Entities
{
    public class Notice : EntityBase { 
        public ObjectId ContainerId { get; set; }
        public ObjectId TripId { get; set; }

    }
}
