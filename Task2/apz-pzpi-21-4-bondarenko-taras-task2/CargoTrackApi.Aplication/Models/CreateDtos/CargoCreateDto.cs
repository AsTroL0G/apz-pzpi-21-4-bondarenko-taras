using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.CreateDtos
{
    public class CargoCreateDto
    {
        public string Name { get; set; }
        public float Weight { get; set; }

        public float Length { get; set; }

        public float Height { get; set; }

        public float Width { get; set; }

        public float AnnouncedPrice { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }
        public string CitySenderId { get; set; }

        public string AddressSenderId { get; set; }
        public string CityReceiverId { get; set; }

        public string AddressReceiverId { get; set; }
    }
}
