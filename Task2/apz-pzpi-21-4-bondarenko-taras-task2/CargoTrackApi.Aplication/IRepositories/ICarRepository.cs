using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<Car> UpdateCar(Car car, CancellationToken cancellationToken);

    }
}
