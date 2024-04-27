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
    public class DriverRepository : BaseRepository<Driver>, IDriverRepository
    {
        public DriverRepository(MongoDbContext db) : base(db, "Driver") { }

        public async Task<Driver> UpdateDriver(Driver driver, CancellationToken cancellationToken)
        {
            var updateDefinition = Builders<Driver>.Update
                .Set(u => u.FirstName, driver.FirstName)
                .Set(u => u.Email, driver.Email)
                .Set(u => u.LastName, driver.LastName)
                .Set(u => u.Patronym, driver.Patronym)
                .Set(u => u.PhoneNumber, driver.PhoneNumber)
                .Set(u => u.PasswordHash, driver.PasswordHash)
                .Set(u => u.DriverLicenseNumber, driver.DriverLicenseNumber)
                .Set(u => u.LastModifiedDateUtc, driver.LastModifiedDateUtc)
                .Set(u => u.LastModifiedById, driver.LastModifiedById);

            var options = new FindOneAndUpdateOptions<Driver>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(
                Builders<Driver>.Filter.Eq(u => u.Id, driver.Id),
                updateDefinition,
                options,
                cancellationToken);
        }
    }
}