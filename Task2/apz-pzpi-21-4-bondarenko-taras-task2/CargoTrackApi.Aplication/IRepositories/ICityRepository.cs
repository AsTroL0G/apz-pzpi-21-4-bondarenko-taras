using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{
    public interface ICityRepository : IBaseRepository<City>
    {
        Task<City> UpdateCity(City city, CancellationToken cancellationToken);
    }
}
