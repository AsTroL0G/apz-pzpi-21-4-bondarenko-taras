using CargoTrackApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Domain.Entities
{
    public class City : EntityBase
    {
        public string Name { get; set; }
        public string District { get; set; }
        public string Сountry { get; set; }
        
    }


}
