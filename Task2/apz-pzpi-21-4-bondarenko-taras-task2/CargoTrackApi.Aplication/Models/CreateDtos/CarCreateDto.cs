using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.CreateDtos
{
    public class CarCreateDto
    {
        public string Number_MIA { get; set; }

        public string Brand { get; set; }

        public float LoadCapacity { get; set; }
    }

}
