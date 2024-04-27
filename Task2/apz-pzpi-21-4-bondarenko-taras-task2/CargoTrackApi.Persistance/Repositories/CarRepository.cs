using CargoTrackApi.Domain.Common;
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

    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(MongoDbContext db) : base(db, "Car") { }

        public async Task<Car> UpdateCar(Car car, CancellationToken cancellationToken)
        {
            var updateDefinition = MongoDB.Driver.Builders<Car>.Update
                .Set(c => c.Number_MIA, car.Number_MIA)
                .Set(c => c.Brand, car.Brand)
                .Set(c => c.LoadCapacity, car.LoadCapacity);


            var options = new MongoDB.Driver.FindOneAndUpdateOptions<Car>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Car>.Filter.Eq(u => u.Id, car.Id),
                updateDefinition,
                options,
                cancellationToken);
        }
    }
}