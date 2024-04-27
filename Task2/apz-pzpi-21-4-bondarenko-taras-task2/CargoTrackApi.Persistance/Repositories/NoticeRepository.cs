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
    public class NoticeRepository : BaseRepository<Notice>, INoticeRepository
    {
        public NoticeRepository(MongoDbContext db) : base(db, "Notice") { }

        public async Task<Notice> UpdateNotice(Notice notice, CancellationToken cancellationToken)
        {
            var updateDefinition = Builders<Notice>.Update
                .Set(u => u.ContainerId, notice.ContainerId)
                .Set(u => u.TripId, notice.TripId);

            var options = new FindOneAndUpdateOptions<Notice>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(
                Builders<Notice>.Filter.Eq(u => u.Id, notice.Id),
                updateDefinition,
                options,
                cancellationToken);
        }
        public async Task<Notice> GetContainer(string tripId, CancellationToken cancellationToken)
        {
            return await this._collection.Find(c => c.TripId == ObjectId.Parse(tripId)).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Notice>> GetTripByContainerId(string containerId, CancellationToken cancellationToken)
        {
            var filter = Builders<Notice>.Filter.Eq(item => item.ContainerId, ObjectId.Parse(containerId));

            var trip = await _collection.Find(filter)
                .ToListAsync(cancellationToken);
            return trip;
        }

    }
}
