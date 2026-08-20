using Microsoft.Extensions.DependencyInjection;
using ShipManagement.Application.Interfaces;
using ShipManagement.Application.Services;

namespace ShipManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IShipService, ShipService>();

        return services;
    }
}