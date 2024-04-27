using CargoTrackApi.Domain.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Domain.Entities
{
    public class Cargo : EntityBase
    {
        public string Name { get; set; }
        public float Weight { get; set; }

        public float Length { get; set; }

        public float Height { get; set; }

        public float Width { get; set; }

        public float AnnouncedPrice { get; set; }

        public float? ShippingPrice { get; set; }

        public ObjectId? NoticeId { get; set; }

        public ObjectId SenderId { get; set; }

        public ObjectId ReceiverId { get; set; }

        public ObjectId CitySenderId { get; set; }

        public string AddressSenderId { get; set; }
        public ObjectId CityReceiverId { get; set; }

        public string AddressReceiverId { get; set; }


    }
}
