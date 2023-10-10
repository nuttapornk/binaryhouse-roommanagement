using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoomManagement.Helpers.HealthCheck;
using RoomManagement.Helpers.NSwag;
using RoomManagement.Middleware;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors.Security;

namespace RoomManagement;

public static class ConfigureService
{
    public static IServiceCollection AddComponentService(this IServiceCollection services,
    IConfiguration configuration, string environmentName)
    {
        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddCheck<AliveHealthCheck>("alive", tags: new[] { "alive" });


        services.AddCors(options =>
        {
            options.AddPolicy("CORS", policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });
        });

        services.AddOpenApiDocument(config =>
        {
            config.Title = "RoomManagement API";
            config.Version = "v1";
            config.Description = $"RoomManagement API {environmentName}";
            config.OperationProcessors.Add(new AddRequiredHeaderParameter());

            ////Add Header Authorization JWT
            // config.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            // {
            //     Type = OpenApiSecuritySchemeType.ApiKey,
            //     Name = "Authorization",
            //     In = OpenApiSecurityApiKeyLocation.Header,
            //     Description = "Type into the textbox: Bearer {your JWT token}."
            // });
            // config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));

        });

        //Middleware
        services.AddTransient<RequestHeaderMiddleware>();

        return services;
    }
}
