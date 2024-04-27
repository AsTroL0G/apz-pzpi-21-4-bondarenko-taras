using CargoTrackApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Domain.Entities
{
    public class Sensor : EntityBase
    {
        public string? Type { get; set; }

        public string? Name { get; set; }

    }
}
