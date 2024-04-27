using CargoTrackApi.Persistance.Database;
using Microsoft.Extensions.DependencyInjection;
using CargoTrackApi.Application.IRepositories;
using CargoTrackApi.Persistance.Repositories;

namespace CargoTrackApi.Persistance.PersistanceExtentions
{
    public static class RepositoriesExtention
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<MongoDbContext>();

            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
            services.AddScoped<IContainerRepository, ContainerRepository>();
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<IValueRepository, ValueRepository>();
            services.AddScoped<ISensorsRepository, SensorsRepository>();
            services.AddScoped<ITripRepository,TripRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<INoticeRepository, NoticeRepository>();
            return services;
        }
    }
}
