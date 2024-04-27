using CargoTrackApi.Domain.Common;
using CargoTrackApi.Domain.Entities;
using CargoTrackApi.Persistance.Database;
using CargoTrackApi.Application.IRepositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CargoTrackApi.Application.Models.Dtos;
using System.Threading;

namespace CargoTrackApi.Persistance.Repositories
{
    public class ContainerRepository : BaseRepository<Container>, IContainerRepository
    {
        public ContainerRepository(MongoDbContext db) : base(db, "Container") { }

        public async Task<Container> UpdateContainer(Container container, CancellationToken cancellationToken)
        {
            var updateDefinition = MongoDB.Driver.Builders<Container>.Update
                .Set(c => c.Name, container.Name)
                .Set(c => c.Type, container.Type)
                .Set(c => c.LoadCapacity, container.LoadCapacity)
                .Set(c => c.Status, container.Status);



            var options = new MongoDB.Driver.FindOneAndUpdateOptions<Container>
            {
                ReturnDocument = ReturnDocument.After
            };

            return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Container>.Filter.Eq(u => u.Id, container.Id),
                updateDefinition,
                options,
                cancellationToken);
        }

    }


}
