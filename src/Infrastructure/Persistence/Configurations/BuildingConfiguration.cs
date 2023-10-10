using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable("Building");

        builder.Property(t => t.Name)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(t => t.CreateUser)
        .HasMaxLength(100);

        builder.Property(t => t.UpdateUser)
        .HasMaxLength(100);

    }
}
