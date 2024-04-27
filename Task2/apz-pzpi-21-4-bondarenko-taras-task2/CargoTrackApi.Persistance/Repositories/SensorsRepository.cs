using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Domain.Entities;
using CargoTrackApi.Persistance.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Persistance.Repositories
{
    public class SensorsRepository : BaseRepository<Sensors>, ISensorsRepository
    {
        public SensorsRepository(MongoDbContext db) : base(db, "Sensors") { }

        public async Task<Sensors> UpdateSensors(Sensors dto, CancellationToken cancellationToken)
        {

            var updateDefinition = MongoDB.Driver.Builders<Sensors>.Update
            .Set(c => c.ContainerId, dto.ContainerId)
            .Set(c => c.SensorId, dto.SensorId);


            var options = new MongoDB.Driver.FindOneAndUpdateOptions<Sensors>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Sensors>.Filter.Eq(u => u.Id, dto.Id),
                updateDefinition,
                options,
                cancellationToken);
        }
        public async Task<List<Sensors>> GetSensorsById(string containerId, CancellationToken cancellationToken)
        {
            return await this._collection.FindAsync(c => c.ContainerId == ObjectId.Parse(containerId)).Result.ToListAsync();
        }
        public async Task<List<Sensors>> FindSensorsByGPS(List<Sensor> sensorList, CancellationToken cancellationToken)
        {
            var sensorIds = sensorList.Select(s => s.Id).ToList();
            var filters = Builders<Sensors>.Filter.In(sensor => sensor.SensorId, sensorIds);
            var sensors = await this._collection.Find(filters).ToListAsync(cancellationToken);
            return sensors;
        }
        public async Task<Sensors> GetSensorsByType(List<Sensors> sensorList, string containerId, CancellationToken cancellationToken)
        {
            var sensorIds = sensorList.Select(s => s.Id).ToList();
            var filters = Builders<Sensors>.Filter.Eq(s => s.ContainerId, ObjectId.Parse(containerId));


            var sensor = await this._collection.Find(filters).FirstOrDefaultAsync(cancellationToken);
            return sensor;
        }




    }
}
