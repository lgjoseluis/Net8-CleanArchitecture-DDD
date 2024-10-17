using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Alfa.CarRental.Application.Abstractions.Clock;
using Alfa.CarRental.Infrastructure.Clock;
using Alfa.CarRental.Application.Abstractions.EMail;
using Alfa.CarRental.Infrastructure.EMail;
using Alfa.CarRental.Domain.Users;
using Alfa.CarRental.Infrastructure.Repositories;
using Alfa.CarRental.Domain.Vehicles;
using Alfa.CarRental.Domain.Rentals;
using Alfa.CarRental.Domain.Abstractions;
using Dapper;
using Alfa.CarRental.Infrastructure.Data;
using Alfa.CarRental.Application.Abstractions.Data;

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

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => {
            return new SqlConnectionFactory(connectionString);
        });

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }
}
