using Application.Common.Caching.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Database;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Caching;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ApplicationDbContext"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
        );

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        //services.ConfigRedis(configuration);

        // var externalApiSettings = new List<ExternalApiSetting>();
        // configuration.GetSection("ExternalApi").Bind(externalApiSettings);

        // //ExternalApi
        // var theCatApiSetting = externalApiSettings.FirstOrDefault(a => a.Name == "TheCatApi")
        // ?? throw new Exception("Appsetting ExternalApi TheCatApi is Empty.");
        //ConfigTheCatApi(services);

        services.ConfigRepositories();
        return services;
    }

    private static IServiceCollection ConfigRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedMemoryCache();
        services.AddTransient<IRedisCacheService, RedisCacheService>();

        var redisEndPoint = configuration["Redis:EndPointHost"];


        var redisConfigure = $"{redisEndPoint}," +
            $"channelPrefix={configuration["Redis:ChannelPrefix"]}," +
            $"defaultDatabase={configuration["Redis:DefaultDatabase"]}";

        services.AddStackExchangeRedisCache(cfg =>
        {
            cfg.Configuration = redisConfigure;
        });

        return services;
    }

    private static IServiceCollection ConfigRepositories(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        // services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();
        return services;
    }

}
