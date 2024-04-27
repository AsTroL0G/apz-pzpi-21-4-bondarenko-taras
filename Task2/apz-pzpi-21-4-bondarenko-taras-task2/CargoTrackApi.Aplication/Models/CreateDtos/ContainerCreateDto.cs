using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.CreateDtos
{
    public class ContainerCreateDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public float LoadCapacity { get; set; }
        public float Length { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public string Status { get; set; }
    }
}
