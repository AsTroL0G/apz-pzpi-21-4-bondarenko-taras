using CargoTrackApi.Domain.Common;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{

    public interface IBaseRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity> GetOneAsync(ObjectId id, CancellationToken cancellationToken);

        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<List<TEntity>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<List<TEntity>> GetPageAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<int> GetTotalCountAsync(CancellationToken cancellationToken);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> DeleteFromDbAsync(TEntity entity, CancellationToken cancellationToken);

    }

}
