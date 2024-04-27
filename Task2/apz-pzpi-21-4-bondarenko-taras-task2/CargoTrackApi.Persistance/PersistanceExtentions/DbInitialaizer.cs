using CargoTrackApi.Persistance.Database;
using CargoTrackApi.Infrastructure.Services.Identity;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using CargoTrackApi.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MongoDB.Driver.WriteConcern;
using Microsoft.Extensions.DependencyInjection;

namespace CargoTrackApi.Persistance.PersistanceExtentions
{
    public class DbInitialaizer
    {
        private readonly IMongoCollection<User> _userCollection;

        private readonly IMongoCollection<Role> _roleCollection;

        private readonly IMongoCollection<Container> _containerCollection;

        private readonly IMongoCollection<Value> _valueCollection;

        private readonly IMongoCollection<Sensor> _sensorCollection;

        private readonly IMongoCollection<Sensors> _sensorsCollection;

        private readonly IMongoCollection<Trip> _tripCollection;

        private readonly IMongoCollection<Schedule> _scheduleCollection;

        private readonly IMongoCollection<Driver> _driverCollection;

        private readonly IMongoCollection<City> _cityCollection;

        private readonly IMongoCollection<Car> _carCollection;

        private readonly IMongoCollection<Cargo> _cargoCollection;

        private readonly IMongoCollection<Notice> _noticeCollection;

        private readonly PasswordHasher passwordHasher;

        public DbInitialaizer(IServiceProvider serviceProvider)
        {

            passwordHasher = new PasswordHasher(new Logger<PasswordHasher>(new LoggerFactory()));

            _userCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<User>("Users");

            _roleCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Role>("Role");

            _containerCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Container>("Container");

            _valueCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Value>("Value");

            _sensorCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Sensor>("Sensor");

            _sensorsCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Sensors>("ContainerSensor");

            _tripCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Trip>("Trip");

            _scheduleCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Schedule>("Schedule");

            _driverCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Driver>("Driver");

            _cityCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<City>("City");

            _carCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Car>("Car");

            _cargoCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Cargo>("Cargo");

            _noticeCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<Notice>("Notice");
        }
    }
}
