using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Alfa.CarRental.Application.Abstractions.Clock;
using Alfa.CarRental.Infrastructure.Clock;
using Alfa.CarRental.Application.Abstractions.EMail;
using Alfa.CarRental.Infrastructure.EMail;
using Microsoft.EntityFrameworkCore;

namespace Alfa.CarRental.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection InfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("Database") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        return services;
    }
}
