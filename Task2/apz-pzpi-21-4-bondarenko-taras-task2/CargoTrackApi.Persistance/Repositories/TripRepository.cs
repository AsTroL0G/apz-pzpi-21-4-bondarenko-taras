using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Domain.Entities;
using CargoTrackApi.Persistance.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Persistance.Repositories
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        public TripRepository(MongoDbContext db) : base(db, "Trip") { }

        public async Task<Trip> UpdateTrip(Trip trip, CancellationToken cancellationToken)
        {
            var updateDefinition = Builders<Trip>.Update
                .Set(u => u.CarId, trip.CarId)
                .Set(u => u.DriverId, trip.DriverId);


        var options = new FindOneAndUpdateOptions<Trip>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(
                Builders<Trip>.Filter.Eq(u => u.Id, trip.Id),
                updateDefinition,
                options,
                cancellationToken);
        }
    }
}
