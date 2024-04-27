﻿using CargoTrackApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackApi.Application.IRepositories
{

    public interface ISensorRepository : IBaseRepository<Sensor>
    {
        Task<Sensor> UpdateSensor(Sensor sensor, CancellationToken cancellationToken);
        Task<Sensor> GetSensor(string sensorId, CancellationToken cancellationToken);
        Task<List<Sensor>> GetSensorByType(string type, CancellationToken cancellationToken);

    }
}
