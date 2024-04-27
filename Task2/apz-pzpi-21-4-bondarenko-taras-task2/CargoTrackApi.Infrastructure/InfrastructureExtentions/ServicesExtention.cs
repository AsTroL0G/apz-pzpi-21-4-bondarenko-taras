using Microsoft.Extensions.DependencyInjection;
using CargoTrackApi.Application.IServices;
using CargoTrackApi.Infrastructure.Services;
using IncubatorApi.Infrastructure.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CargoTrackApi.Application.IServices.Identity;
using CargoTrackApi.Infrastructure.Services.Identity;
using CargoTrackApi.Application.IServices.StatisticsService;
using CargoTrackApi.Domain.Entities;
using System.ComponentModel;
using CargoTrackApi.Aplication.IServices;
using CargoTrackApi.Infrastructure.Services.Statistics;

namespace CargoTrackApi.Infrastructure.InfrastructureExtentions
{
    public static class ServicesExtention
    {

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokensService, TokensService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IContainerService, ContainerService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<ITripService,TripService>();
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IValueService, ValueServices>();
            services.AddScoped<ISensorsService, SensorsServices>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICargoService, CargoService>();
            services.AddScoped<INoticeService, NoticeService>();
            services.AddScoped<IStatisticsService, StatisticsService>();

            return services;
        }


        public static IServiceCollection AddJWTTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = configuration.GetValue<bool>("JsonWebTokenKeys:ValidateIssuer"),
                        ValidateAudience = configuration.GetValue<bool>("JsonWebTokenKeys:ValidateAudience"),
                        ValidateLifetime = configuration.GetValue<bool>("JsonWebTokenKeys:ValidateLifetime"),
                        ValidateIssuerSigningKey = configuration.GetValue<bool>("JsonWebTokenKeys:ValidateIssuerSigningKey"),
                        ValidIssuer = configuration.GetValue<string>("JsonWebTokenKeys:ValidIssuer"),
                        ValidAudience = configuration.GetValue<string>("JsonWebTokenKeys:ValidAudience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JsonWebTokenKeys:IssuerSigningKey"))),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
