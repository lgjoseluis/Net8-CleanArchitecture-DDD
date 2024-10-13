using Alfa.CarRental.Domain.Rentals;
using Microsoft.Extensions.DependencyInjection;

namespace Alfa.CarRental.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR( configuration => {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddTransient<PriceService>();

        return services;
    }
}
