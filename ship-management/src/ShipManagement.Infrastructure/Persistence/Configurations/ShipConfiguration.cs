using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipManagement.Domain.Entities;

namespace ShipManagement.Infrastructure.Persistence.Configurations;

public sealed class ShipConfiguration
    : IEntityTypeConfiguration<Ship>
{
    public void Configure(
        EntityTypeBuilder<Ship> builder)
    {
        builder.ToTable("Ships");

        builder.HasKey(ship => ship.Id);

        builder.Property(ship => ship.Id)
            .ValueGeneratedNever();

        builder.Property(ship => ship.ShipName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(ship => ship.IMO)
            .HasMaxLength(7)
            .IsRequired();

        builder.Property(ship => ship.GrossTonnage)
            .IsRequired();

        builder.HasIndex(ship => ship.IMO)
            .IsUnique();
    }
}