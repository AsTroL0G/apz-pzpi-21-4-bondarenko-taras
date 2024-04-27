using CargoTrackApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);
    }

}
