using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class WeatherForecastCreatedEvent : BaseEvent
{
    public WeatherForecastCreatedEvent(WeatherForecast item)
    {
        Item = item;
    }

    public WeatherForecast Item { get; }
}
