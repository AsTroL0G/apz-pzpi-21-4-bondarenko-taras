using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.Dtos
{
    public class CarDto
    {
        public string Id { get; set; }

        public string Number_MIA { get; set; }

        public string Brand { get; set; }

        public float LoadCapacity { get; set; }
    }
}
