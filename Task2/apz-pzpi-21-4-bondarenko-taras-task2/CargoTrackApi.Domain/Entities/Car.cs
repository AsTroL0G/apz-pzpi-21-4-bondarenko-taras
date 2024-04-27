using CargoTrackApi.Domain.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Domain.Entities
{
    public class Car : EntityBase
    {
        public string Number_MIA { get; set; }

        public string Brand { get; set; }

        public float LoadCapacity { get; set; }

    }
}
