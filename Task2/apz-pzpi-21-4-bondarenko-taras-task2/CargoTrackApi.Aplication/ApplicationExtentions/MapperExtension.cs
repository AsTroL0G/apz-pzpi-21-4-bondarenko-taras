using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using CargoTrackApi.Application.Mapping;
using CargoTrackApi.Domain.Entities;

namespace CargoTrackApi.Application.ApplicationExtentions;

public static class MapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(UserProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(SensorProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(ValueProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(SensorsProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(CarProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(CityProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(ContainerProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(DriverProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(ScheduleProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(TripProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(CargoProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(NoticeProfile)));
        return services;
    }
}