using CargoTrackApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.Models.CreateDtos
{
    public class DriverCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronym { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string DriverLicenseNumber { get; set; }
    }
}
