using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.Dtos
{
    public class TripDto
    {
        public string Id { get; set; }
        public string CarId { get; set; }
        public string DriverId { get; set; }
    }
}
