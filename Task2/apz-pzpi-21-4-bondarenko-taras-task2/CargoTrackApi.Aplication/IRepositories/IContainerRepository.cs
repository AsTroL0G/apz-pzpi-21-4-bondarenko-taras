using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Application.Models.Dtos;
using CargoTrackApi.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{
    public interface IContainerRepository : IBaseRepository<Container>
    {
        Task<Container> UpdateContainer(Container container, CancellationToken cancellationToken);

    }
}
