using MongoDB.Driver;

namespace CargoTrackApi.Persistance.Database
{
    public class MongoDbContext
    {
        private readonly MongoClient _client;

        private readonly IMongoDatabase _db;

        public MongoDbContext()
        {
            this._client = new MongoClient("mongodb://localhost:27017");
            this._db = this._client.GetDatabase("CargoTrack");
        }

        public IMongoDatabase Db => this._db;

        public MongoClient Client => this._client;
    }
}
