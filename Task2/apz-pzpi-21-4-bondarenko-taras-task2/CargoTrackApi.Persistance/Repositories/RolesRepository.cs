using CargoTrackApi.Domain.Entities;
using CargoTrackApi.Persistance.Database;
using CargoTrackApi.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Persistance.Repositories
{
    public class RolesRepository : BaseRepository<Role>, IRolesRepository
    {
        public RolesRepository(MongoDbContext db) : base(db, "Role") { }
    }
}
