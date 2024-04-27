using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.UpdateDto
{

    public class SensorUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }
    }

}
