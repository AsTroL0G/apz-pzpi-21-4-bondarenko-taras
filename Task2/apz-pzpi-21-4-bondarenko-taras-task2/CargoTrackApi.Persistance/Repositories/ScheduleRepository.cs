using CargoTrackApi.Persistance.Database;
using CargoTrackApi.Domain.Entities;
using CargoTrackApi.Application.IRepositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CargoTrackApi.Persistance.Repositories
{

    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(MongoDbContext db) : base(db, "Schedule") { }

        public async Task<Schedule> UpdateSchedule(Schedule schedule, CancellationToken cancellationToken)
        {
            var updateDefinition = Builders<Schedule>.Update
                .Set(u => u.TripId, schedule.TripId)
                .Set(u => u.CityId, schedule.CityId)
                .Set(u => u.ArrivalTime, schedule.ArrivalTime);


            var options = new FindOneAndUpdateOptions<Schedule>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(
                Builders<Schedule>.Filter.Eq(u => u.Id, schedule.Id),
                updateDefinition,
                options,
                cancellationToken);

        }
        public async Task<List<Schedule>> GetScheduleByTripId(string tripId, CancellationToken cancellationToken)
        {

            var filter = Builders<Schedule>.Filter.Eq(item => item.TripId, ObjectId.Parse(tripId));

            var schedule = await _collection.Find(filter)
                .ToListAsync(cancellationToken);
            return schedule;
        }

    }
}