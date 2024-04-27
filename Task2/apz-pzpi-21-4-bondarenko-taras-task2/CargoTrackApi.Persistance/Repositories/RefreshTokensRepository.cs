using CargoTrackApi.Persistance.Database;
using CargoTrackApi.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoTrackApi.Domain.Entities;

namespace CargoTrackApi.Persistance.Repositories
{
    public class RefreshTokensRepository : BaseRepository<RefreshToken>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(MongoDbContext db) : base(db, "RefreshTokens") { }
    }

}
