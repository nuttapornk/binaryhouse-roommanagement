using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IWeatherForecastRepository : IDisposable
{
    Task<IEnumerable<WeatherForecast>> FindAllAsync();
    Task<WeatherForecast?> FindByIdAsync(int id);
    Task<bool> IsWeatherForecastNotExistByDate(DateTime date, CancellationToken cancellationToken);
    Task<bool> IsWeatherForecastNotExistByDate(DateTime date, int id, CancellationToken cancellationToken);
    Task<int> CreateAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken);
    Task<int> DeleteAsync(int weatherForecastId, CancellationToken cancellationToken);
    Task<int> UpdateAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken);
}
