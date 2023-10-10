using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces.Database;

public interface IApplicationDbContext
{
    //DbSet<WeatherForecast> WeatherForecasts { get; }
    DbSet<Building> Buildings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    void Dispose();
}
