using Domain.Common;

namespace Domain.Entities;

public class WeatherForecast : BaseAuditableEntity
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summaries { get; set; }
}
