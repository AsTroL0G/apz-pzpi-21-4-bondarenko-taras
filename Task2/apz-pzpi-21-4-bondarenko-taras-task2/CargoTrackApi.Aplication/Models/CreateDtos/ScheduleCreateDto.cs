using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.CreateDtos
{

    public class ScheduleCreateDto
    {
        public string TripId { get; set; }
        public string CityId { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}

