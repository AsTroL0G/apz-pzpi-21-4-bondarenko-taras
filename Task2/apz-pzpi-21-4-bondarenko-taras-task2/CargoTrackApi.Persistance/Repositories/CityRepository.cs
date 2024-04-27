using CargoTrackApi.Domain.Entities;
using CargoTrackApi.Persistance.Database;
using CargoTrackApi.Application.IRepositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Persistance.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(MongoDbContext db) : base(db, "City") { }

        public async Task<City> UpdateCity(City city, CancellationToken cancellationToken)
        {
            var updateDefinition = MongoDB.Driver.Builders<City>.Update
                .Set(c => c.Name, city.Name)
                .Set(c => c.District, city.District)
                .Set(c => c.Сountry, city.Сountry);

        var options = new MongoDB.Driver.FindOneAndUpdateOptions<City>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<City>.Filter.Eq(u => u.Id, city.Id),
                updateDefinition,
                options,
                cancellationToken);
        }
    }
}
