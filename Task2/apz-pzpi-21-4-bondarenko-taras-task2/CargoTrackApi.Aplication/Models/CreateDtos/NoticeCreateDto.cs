using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.CreateDtos
{
    public class NoticeCreateDto
    {
        public string ContainerId { get; set; }
        public string TripId { get; set; }
    }
}
