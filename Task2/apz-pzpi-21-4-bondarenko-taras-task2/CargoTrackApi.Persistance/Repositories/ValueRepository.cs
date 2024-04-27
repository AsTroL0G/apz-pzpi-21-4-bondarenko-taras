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
    public class ValueRepository : BaseRepository<Value>, IValueRepository
    {
        public ValueRepository(MongoDbContext db) : base(db, "Value") { }

        public async Task<Value> UpdateValue(Value dto, CancellationToken cancellationToken)
        {

            var updateDefinition = MongoDB.Driver.Builders<Value>.Update
            .Set(c => c.Values, dto.Values)
            .Set(c => c.MeasurementTime, dto.MeasurementTime)
            .Set(c => c.SensorId, dto.SensorId);


            var options = new MongoDB.Driver.FindOneAndUpdateOptions<Value>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Value>.Filter.Eq(u => u.Id, dto.Id),
                updateDefinition,
                options,
                cancellationToken);
        }
        public async Task<List<Value>> GetValueBySensorId(string sensorId, CancellationToken cancellationToken)
        {
            return await this._collection.FindAsync(c => c.SensorId == ObjectId.Parse(sensorId)).Result.ToListAsync();
        }

        public async Task<List<ContainerCoordinatesDto>> GetLatestValueBySensorId(List<Sensors> sensorsList, CancellationToken cancellationToken)
        {
            var result = new List<ContainerCoordinatesDto>();

            foreach (var sensor in sensorsList)
            {
                var sensorId = sensor.SensorId;
                var filter = Builders<Value>.Filter.Eq(v => v.SensorId, sensorId);
                var sortByLatest = Builders<Value>.Sort.Descending(v => v.MeasurementTime);

                var value = await this._collection
                    .Find(filter)
                    .Sort(sortByLatest)
                    .Limit(1)
                    .SingleOrDefaultAsync(cancellationToken);

                if (value != null)
                {
                    result.Add(new ContainerCoordinatesDto
                    {
                        ContainerId = sensor.ContainerId.ToString(),
                        Coordinates = value.Values
                    });
                }
            }

            return result;
        }
        public async Task<List<ContainerCoordinatesDto>> GetNewestValueBySensorId(List<Sensors> sensorsList, CancellationToken cancellationToken)
        {
            var result = new List<ContainerCoordinatesDto>();

            foreach (var sensor in sensorsList)
            {
                var sensorId = sensor.SensorId;
                var filter = Builders<Value>.Filter.Eq(v => v.SensorId, sensorId);
                var sortByOldest = Builders<Value>.Sort.Ascending(v => v.MeasurementTime);

                var value = await this._collection
                    .Find(filter)
                    .Sort(sortByOldest)
                    .Limit(1)
                    .SingleOrDefaultAsync(cancellationToken);

                if (value != null)
                {
                    result.Add(new ContainerCoordinatesDto
                    {
                        ContainerId = sensor.ContainerId.ToString(),
                        Coordinates = value.Values
                    });
                }
            }

            return result;
        }

    }
}
