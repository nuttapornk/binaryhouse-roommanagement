using Application;
using Infrastructure;
using RoomManagement;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using RoomManagement.Helpers.HealthCheck;
using RoomManagement.Middleware;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddComponentService(builder.Configuration, builder.Environment.EnvironmentName);
builder.Services.AddControllers();

var app = builder.Build();

//Middleware
app.UseMiddleware<RequestHeaderMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
    app.UseReDoc();
}
app.UseCors("CORS");

//HealtCheck
app.UseHealthChecks("/health");
app.MapHealthChecks("/alive", new HealthCheckOptions
{
    //Predicate = healthCheck => healthCheck.Tags.Contains("ready"),
    ResponseWriter = HealthCheckAlive.WriteAsync,
});

MigrationsDatabase(app);

app.MapControllers();
app.Run();

static void MigrationsDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An Error occurred sedding the Database");
    }
}

namespace RoomManagement
{
    public partial class Program { }
}