using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Application.Models.Dtos;
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
    public class SensorRepository : BaseRepository<Sensor>, ISensorRepository
    {
        public SensorRepository(MongoDbContext db) : base(db, "Sensor") { }

        public async Task<Sensor> UpdateSensor(Sensor dto, CancellationToken cancellationToken)
        {

            var updateDefinition = MongoDB.Driver.Builders<Sensor>.Update
                .Set(c => c.Name, dto.Name)
                .Set(c => c.Type, dto.Type);


            var options = new MongoDB.Driver.FindOneAndUpdateOptions<Sensor>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Sensor>.Filter.Eq(u => u.Id, dto.Id),
                updateDefinition,
                options,
                cancellationToken);
        }
        public async Task<Sensor> GetSensor(string sensorId, CancellationToken cancellationToken)
        {
            var result = await this._collection.FindAsync(c => c.Id == ObjectId.Parse(sensorId), cancellationToken: cancellationToken);
            return await result.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<List<Sensor>> GetSensorByType(string type, CancellationToken cancellationToken)
        {
            var result = await this._collection.FindAsync(c => c.Type == type, cancellationToken: cancellationToken);
            return await result.ToListAsync(cancellationToken);
        }

    }
}
