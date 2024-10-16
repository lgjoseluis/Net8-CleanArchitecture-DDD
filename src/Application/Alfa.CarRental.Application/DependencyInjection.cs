using Alfa.CarRental.Application.Abstractions.Behaviors;
using Alfa.CarRental.Domain.Rentals;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Alfa.CarRental.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR( configuration => {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(LogginBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddTransient<PriceService>();

        return services;
    }
}
