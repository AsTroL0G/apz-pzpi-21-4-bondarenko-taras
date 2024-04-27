using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.Dtos
{
    public class CityDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string District { get; set; }
        public string Сountry { get; set; }
    }
}
