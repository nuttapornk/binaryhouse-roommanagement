using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.ToTable("WeatherForecast");

        builder.Property(t => t.Summaries)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(t => t.TemperatureC)
        .IsRequired();

    }
}
