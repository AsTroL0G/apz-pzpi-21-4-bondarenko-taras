using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IServices.Identity
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Check(string password, string passwordHash);
    }
}
