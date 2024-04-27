using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.Dtos
{
    public class ContainerCoordinatesDto
    {
        public string ContainerId { get; set; }
        public string Coordinates { get; set; }
        public string? Distance { get; set; }
    }
}
