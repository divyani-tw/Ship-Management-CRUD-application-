using Microsoft.EntityFrameworkCore;
using ShipManagement.Domain.Entities;

namespace ShipManagement.Infrastructure.Persistence;

public sealed class ShipManagementDbContext : DbContext
{
    public ShipManagementDbContext(
        DbContextOptions<ShipManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ship> Ships => Set<Ship>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ShipManagementDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}