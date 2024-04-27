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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CargoTrackApi.Persistance.Repositories
{

    public class CargoRepository : BaseRepository<Cargo>, ICargoRepository
    {
        public CargoRepository(MongoDbContext db) : base(db, "Cargo") { }

        public async Task<Cargo> UpdateCargo(Cargo cargo, CancellationToken cancellationToken)
        {
            var updateDefinition = MongoDB.Driver.Builders<Cargo>.Update
                .Set(c => c.Name, cargo.Name)
                .Set(c => c.Weight, cargo.Weight)
                .Set(c => c.Length, cargo.Length)
                .Set(c => c.Height, cargo.Height)
                .Set(c => c.Width, cargo.Width)
                .Set(c => c.AnnouncedPrice, cargo.AnnouncedPrice)
                .Set(c => c.ShippingPrice, cargo.ShippingPrice)
                .Set(c => c.NoticeId, cargo.NoticeId)
                .Set(c => c.CitySenderId, cargo.CitySenderId)
                .Set(c => c.AddressSenderId, cargo.AddressSenderId)
                .Set(c => c.CityReceiverId, cargo.CityReceiverId)
                .Set(c => c.AddressReceiverId, cargo.AddressReceiverId)
                .Set(c => c.SenderId, cargo.SenderId)
                .Set(c => c.ReceiverId, cargo.ReceiverId);

        var options = new MongoDB.Driver.FindOneAndUpdateOptions<Cargo>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Cargo>.Filter.Eq(u => u.Id, cargo.Id),
                updateDefinition,
                options,
                cancellationToken);
        }
        public async Task<bool> CargoUpdateNotice(string cargoId, string newNoticeId, CancellationToken cancellationToken)
        {
            var filter = MongoDB.Driver.Builders<Cargo>.Filter.Eq(x => x.Id, ObjectId.Parse(cargoId));
            var update = MongoDB.Driver.Builders<Cargo>.Update.Set(x => x.NoticeId, ObjectId.Parse(newNoticeId));

            var result = await _collection.UpdateOneAsync(filter, update);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        public async Task<List<Cargo>> GetCargoBySender(string senderId, CancellationToken cancellationToken)
        {
            
            var filter = Builders<Cargo>.Filter.Eq(item => item.SenderId, ObjectId.Parse(senderId));

            var cargo = await _collection.Find(filter)
                .ToListAsync(cancellationToken);
            return cargo;
        }

        public async Task<List<Cargo>> GetCargoByReceiver(string receiverId, CancellationToken cancellationToken)
        {
            var filter = Builders<Cargo>.Filter.Eq(item => item.ReceiverId, ObjectId.Parse(receiverId));

            var cargo = await _collection.Find(filter)
                .ToListAsync(cancellationToken);
            return cargo;
        }
        public async Task<List<Cargo>> GetCargoByNotice(string noticeId, CancellationToken cancellationToken)
        {
            var filter = Builders<Cargo>.Filter.Eq(item => item.NoticeId, ObjectId.Parse(noticeId));

            var cargo = await this._collection.Find(filter).ToListAsync(cancellationToken);
            return cargo;
        }

    }
}
