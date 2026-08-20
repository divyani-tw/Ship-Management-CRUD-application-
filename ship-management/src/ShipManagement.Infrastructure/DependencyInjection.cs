using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShipManagement.Application.Interfaces;
using ShipManagement.Infrastructure.Persistence;

namespace ShipManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString(
                "ShipManagementDb");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Database connection string 'ShipManagementDb' was not found.");
        }

        services.AddDbContext<ShipManagementDbContext>(
            options =>
            {
                options.UseSqlServer(connectionString);
            });

        services.AddScoped<IShipRepository, ShipRepository>();

        return services;
    }
}