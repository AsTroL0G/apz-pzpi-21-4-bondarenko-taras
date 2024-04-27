using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.CreateDtos
{
    public class TripCreateDto
    {
        public string CarId { get; set; }
        public string DriverId { get; set; }
    }
}
