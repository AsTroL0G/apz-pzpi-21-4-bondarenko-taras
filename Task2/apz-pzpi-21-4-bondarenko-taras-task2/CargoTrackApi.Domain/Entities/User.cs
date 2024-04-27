using CargoTrackApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Domain.Entities
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronym { get; set; }
        public List<Role> Roles { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

    }
}
